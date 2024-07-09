using AutoMapper;
using Basket.Domain.Model;
using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using Basket.Infrastructure.Consumer;
using Basket.Infrastructure.Data;
using EventBus.Messages.Event.Basket;
using EventBus.Messages.Event.Store;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Basket.Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BasketdbContext _context;
        private readonly IMapper _mapper;
        private readonly IBasketService _service;
        private readonly ILogger<BasketRepository> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMessageRepository _messageRepository;
        private readonly IRequestClient<CheckStoreEvent> _requestClient;

        public BasketRepository(BasketdbContext context, IMapper mapper, IBasketService service,
            ILogger<BasketRepository> logger, IPublishEndpoint publishEndpoint, IMessageRepository messageRepository,
            IRequestClient<CheckStoreEvent> requestClient)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _messageRepository = messageRepository;
            _requestClient = requestClient;
        }

        public async Task<string> AddBasket(AddItemToBasketDto basketItem)
        {
            var basket =
                await _context.baskets.FirstOrDefaultAsync(x => x.Id == basketItem.BasketId); // search basketId user


            if (basket == null)
            {
                throw new Exception("Basket not found....!");
            }

            var map = _mapper.Map<BasketItem>(basketItem);

            //send check store
            var check = new CheckStoreEvent
            {
                ProductId = map.ProductId,
                Number = map.Quantity
            };
            _logger.LogWarning("---> Send");
            await _publishEndpoint.Publish(check);
            
            // انتظار برای دریافت پاسخ از سرویس فروشگاه
            var m = await _messageRepository.GetMessageResult(map.ProductId.ToString());

            if (m.IsSuccess == false)
            {
                _logger.LogError($"---> {m.Message}");
                return $"{m.Message}";
            }
            
            _context.basketItems.Add(map);

            _logger.LogInformation($"---> add ");
            var productItem = _mapper.Map<ProductDto>(basketItem);

            _service.CreateProduct(productItem);

            await _context.SaveChangesAsync();

            return "Add in Basket";
        }


        public async Task<BasketModelDto> GetOrCreateBasketForUser(string userId)
        {
            var basketUser = await _context.baskets
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.UserId == userId);

            if (basketUser == null)
            {
                return _service.CreateBasketForUser(userId);
            }

            var basketList = new BasketModelDto
            {
                Id = basketUser.Id,
                UserId = userId,
                Items = basketUser.Items.Select(i => new BasketItemDto
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    ProductId = i.ProductId,
                    ProductName = i.Product.ProductName,
                    UnitPrice = i.Product.UnitPrice,
                    ImageUrl = i.Product.ImageUrl,
                }).ToList()
            };

            return basketList;
        }


        public async Task<string> RemoveItemFromBasket(Guid basketId)
        {
            try
            {
                var item = _context.basketItems.FirstOrDefault(x => x.BasketId == basketId);

                if (item == null)
                {
                    return "Not Found Item Id";
                }

                var productItem = await _context.products.SingleOrDefaultAsync(x => x.ProductId == item.ProductId);

                if (item == null)
                    throw new Exception("BasketItem Not Found...!");

                _context.basketItems.Remove(item);
                _context.products.Remove(productItem);
                await _context.SaveChangesAsync();

                return "Item removed successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception($"error : {ex.Message}");
            }
        }


        public async Task<string> UpdateQuantities(Guid basketItemId, int quantity)
        {
            var item = await _context.basketItems.SingleOrDefaultAsync(p => p.Id == basketItemId);
            if (item == null)
            {
                return "Not Found Item.";
            }

            item.SetQuantity(quantity);

            await _context.SaveChangesAsync();
            return "Update Item.";
        }

        public async Task<ResultDto> CheckOutBasket(CheckOutDto checkOut)
        {
            try
            {
                // دریافت سبد خرید
                var getBasket = await _context.baskets
                    .Include(p => p.Items)
                    .ThenInclude(p => p.Product)
                    .SingleOrDefaultAsync(p => p.Id == checkOut.BasketId);

                if (getBasket == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = $"{nameof(getBasket)} Not Found!",
                    };
                }

                var message = _mapper.Map<BasketQueueEvent>(checkOut);

                int priceTotal = 0;
                foreach (var item in getBasket.Items)
                {
                    var basketItem = new BasketItemQueueEvent
                    {
                        BasketItemId = item.Id, // id basketItemId
                        Name = item.Product.ProductName,
                        ProductId = item.Product.ProductId,
                        Price = item.Product.UnitPrice,
                        Quantity = item.Quantity,
                        Total = item.Product.UnitPrice * item.Quantity
                    };
                    priceTotal += item.Product.UnitPrice * item.Quantity;
                    message.TotalPrice = priceTotal;

                    message.BasketItems.Add(basketItem);
                }


                await _context.SaveChangesAsync();

                _logger.LogInformation("----> Publishing message: {@message}", message);
                var pub = _publishEndpoint.Publish(message);
                if (pub != null)
                {
                    foreach (var basketItem in message.BasketItems)
                    {
                        var messageStore = new BasketStoreEvent
                        {
                            ProductId = basketItem.ProductId,
                            Number = basketItem.Quantity
                        };
                        await _publishEndpoint.Publish(messageStore);
                    }

                    await RemoveItemFromBasket(basketId: getBasket.Id);
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = $"no send message."
                    };
                }

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = $"Basket checkout successful and message published."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : {ex.Message}");
                throw ex;
            }
        }
    }
}