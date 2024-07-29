using AutoMapper;
using Basket.Domain.Model;
using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repository.Service;
using Common.Infrastructure.Service;
using Contracts.Basket;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Basket.Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BasketdbContext _context;
        private readonly IMapper _mapper;
        private readonly IBasketProductService _service;
        private readonly ILogger<BasketRepository> _logger;
        private readonly BasketService _basketService;

        public BasketRepository(BasketdbContext context, IMapper mapper, IBasketProductService service,
            ILogger<BasketRepository> logger, BasketService basketService)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
            _logger = logger;
            _basketService = basketService;
        }

        public async Task<string> AddBasket(AddItemToBasketDto basketItem)
        {
            var basket =
                await _context.baskets
                    .Include(b => b.Items)
                    .FirstOrDefaultAsync(x => x.Id == basketItem.BasketId); // search basketId user


            if (basket == null)
            {
                throw new Exception("Basket not found....!");
            }

            var existingItem = basket.Items
                .FirstOrDefault(x => x.ProductId == basketItem.ProductId);

            if (existingItem != null)
            {
                // If the item exists, update the quantity
                var update = await UpdateQuantities(existingItem.Id, basketItem.Quantity);

                return $"{update}";
            }

            var map = _mapper.Map<BasketItem>(basketItem);

            //send check store
            //var check = new CheckStoreEvent
            //{
            //    ProductId = map.ProductId,
            //    Number = map.Quantity
            //};
            //_logger.LogWarning("---> Send");
            //await _publishEndpoint.Publish(check);

            //await Task.Delay(4000);


            var result = await _basketService.CheckStore(map.ProductId, map.Quantity);


            // انتظار برای دریافت پاسخ از سرویس فروشگاه
            //var m = await _messageRepository.GetMessageResult(map.ProductId.ToString());

            //if (m.IsSuccessful == false)
            //{
            //    _logger.LogError($"---> {m.Message}");
            //    return $"{m.Message}";
            //}

            if (result.IsSuccessful == false)
            {
                _logger.LogError($"---> {result.Message}");

                return $"{result.Message}";
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
                    _logger.LogError("Not Found Item Id");
                    return "Not Found Item Id";
                }

                var productItem = await _context.products.SingleOrDefaultAsync(x => x.ProductId == item.ProductId);

                if (item == null)
                {
                    _logger.LogError("BasketItem Not Found...!");
                    throw new Exception("BasketItem Not Found...!");
                }

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
                _logger.LogError("Not Found Item.");
                return "Not Found Item.";
            }

            //send check store
            //var check = new CheckStoreEvent
            //{
            //    ProductId = item.ProductId,
            //    Number = item.Quantity + quantity
            //};
            //await _publishEndpoint.Publish(check);

            //await Task.Delay(4000);

            var result = await _basketService.CheckStore(item.ProductId, item.Quantity);


            // انتظار برای دریافت پاسخ از سرویس فروشگاه

            if (result.IsSuccessful == false)
            {
                _logger.LogError($"---> {result.Message}");

                return $"{result.Message}";
            }

            //var m = await _messageRepository.GetMessageResult(item.ProductId.ToString());

            //if (m.IsSuccessful == false)
            //{
            //    _logger.LogError($"---> {m.Message}");
            //    return $"{m.Message}";
            //}

            item.SetQuantity(quantity);

            await _context.SaveChangesAsync();
            _logger.LogInformation("Update Item.");
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
                        IsSuccessful = false,
                        Message = $"{nameof(getBasket)} Not Found!",
                    };
                }

                var message = _mapper.Map<SendToOrderRequest>(checkOut);

                int priceTotal = 0;
                foreach (var item in getBasket.Items)
                {
                    var basketItem = new BasketItemRequest
                    {
                        BasketItemId = item.Id, // id basketItemId
                        BasketId = item.BasketId,
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

                var resultSend = await _basketService.SendOrder(message);

                //await _publishEndpoint.Publish(message);

                if (resultSend.IsSuccessful)
                {
                    foreach (var basketItem in message.BasketItems)
                    {
                        var messageStore = new CheckOutStoreRequest
                        {
                            ProductId = basketItem.ProductId,
                            Number = basketItem.Quantity
                        };
                        //await _publishEndpoint.Publish(messageStore);

                        var result =
                            await _basketService.UpdateInventoryStore(basketItem.ProductId, basketItem.Quantity);

                        await RemoveItemFromBasket(basketId: basketItem.BasketId);
                    }

                    return new ResultDto
                    {
                        IsSuccessful = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = $"Basket checkout successful and message published."
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccessful = false,
                        StatusCode = resultSend.StatusCode,
                        Message = resultSend.Message
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error : {ex.Message}");
                throw ex;
            }
        }
    }
}