using Basket.Domain.Repository;
using Basket.Infrastructure.Data;

namespace Basket.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly BasketdbContext _context;

    public ProductRepository(BasketdbContext context)
    {
        _context = context;
    }

    public bool UpdateProduct(Guid productId, string productName, int priceProduct)
    {
        var productGet = _context.products.Find(productId);

        if (productGet != null)
        {
            productGet.ProductName = productName;
            productGet.UnitPrice = priceProduct;

            _context.SaveChanges();
            return true;
        }

        return false;
    }
}