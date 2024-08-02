using AutoMapper;
using Basket.Domain.Model;
using Basket.Domain.Model.Dto;
using Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Basket.Infrastructure.Repository.Service
{
    public class BasketProductService : IBasketProductService
    {
        private readonly IMapper _mapper;
        private readonly BasketdbContext _context;

        public BasketProductService(IMapper mapper, BasketdbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public BasketModelDto CreateBasketForUser(string userId)
        {
            var basket = new BasketModel(userId);
            _context.baskets.Add(basket);
            _context.SaveChanges();

            return new BasketModelDto
            {
                UserId = basket.UserId,
                Id = basket.Id,
            };
        }


        public ProductDto CreateProduct(ProductDto product)
        {
            var existProduct = GetProduct(product.ProductId);
            if (existProduct != null)
            {
                return existProduct;
            }
            else
            {
                var newProduct = _mapper.Map<Product>(product);
                _context.Add(newProduct);
                _context.SaveChanges();
                return _mapper.Map<ProductDto>(newProduct);
            }
        }


        public BasketModelDto GetBasket(string UserId)
        {
            var basket = _context.baskets
                .Include(p => p.Items)
                .ThenInclude(p => p.Product)
                .SingleOrDefault(p => p.UserId == UserId);

            if (basket == null)
            {
                return null;
            }

            return new BasketModelDto
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Id = item.Id,
                    Quantity = item.Quantity,

                    ProductName = item.Product.ProductName,
                    UnitPrice = item.Product.UnitPrice,
                    ImageUrl = item.Product.ImageUrl,

                }).ToList(),
            };
        }


        private ProductDto GetProduct(Guid productId)
        {
            var existProduct = _context.products.SingleOrDefault(p => p.ProductId == productId);
            if (existProduct != null)
            {
                var product = _mapper.Map<ProductDto>(existProduct);
                return product;
            }
            else
            {
                return null;
            }
        }

    }
}
