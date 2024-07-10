using Basket.Domain.Model.Dto;


namespace Basket.Infrastructure.Repository.Service
{
    public interface IBasketService
    {
        BasketModelDto CreateBasketForUser(string UserId);
        ProductDto CreateProduct(ProductDto product);
        BasketModelDto GetBasket(string UserId);
    }
}
