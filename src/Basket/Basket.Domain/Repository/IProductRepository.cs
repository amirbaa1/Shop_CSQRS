namespace Basket.Domain.Repository;

public interface IProductRepository
{
    bool UpdateProduct(Guid productId, string productName,int priceProduct);
}