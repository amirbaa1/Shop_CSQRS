using AutoMapper;
using Basket.Domain.Model;
using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using Basket.Infrastructure.Data;
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
        public BasketRepository(BasketdbContext context, IMapper mapper, IBasketService service, ILogger<BasketRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
            _logger = logger;
        }


        public async Task<bool> AddBasket(AddItemToBasketDto basketitem)
        {
            var basket = await _context.baskets.FirstOrDefaultAsync(x => x.Id == basketitem.basketId); // search basketId user


            if (basket == null)
            {
                throw new Exception("Basket not found....!");
            }

            var basketItem = _mapper.Map<BasketItem>(basketitem);


            _context.basketItems.Add(basketItem);
           



            var productItem = _mapper.Map<ProductDto>(basketitem);

            var createProduct = _service.CreateProduct(productItem);
            _context.SaveChanges();

            return true;
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
                    Id = Guid.NewGuid(),
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
                var item = await _context.basketItems.FirstOrDefaultAsync(x => x.BasketId == basketId);

                if (item == null)
                {
                    return await Task.FromResult("Not Found Item Id");
                }
                var productItem = await _context.products.SingleOrDefaultAsync(x => x.ProductId == item.ProductId);

                if (item == null)
                    throw new Exception("BasketItem Not Found...!");

                _context.basketItems.Remove(item);
                _context.products.Remove(productItem);
                await _context.SaveChangesAsync();

                return await Task.FromResult("Item removed successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"error : {ex.Message}");
            }
        }


        public void UpdateQuantities(Guid basketitemId, int quantity)
        {
            var item = _context.basketItems.SingleOrDefault(p => p.Id == basketitemId);
            item.SetQuantity(quantity);
            _context.SaveChanges();
        }
    }

}
