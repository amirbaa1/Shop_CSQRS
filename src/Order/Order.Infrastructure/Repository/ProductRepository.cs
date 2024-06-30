using Order.Domain.Model;
using Order.Domain.Model.Dto;
using Order.Domain.Repository;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly OrderdbContext _orderdbContext;

    public ProductRepository(OrderdbContext orderdbContext)
    {
        _orderdbContext = orderdbContext;
    }

    public Product GetProduct(ProductDto productDto)
    {
        var getProduct = _orderdbContext.Products.SingleOrDefault(x => x.ProductId == productDto.ProductId);
        if (getProduct != null)
        {
            return getProduct;
        }

        return CreateProduct(productDto);
    }

    private Product CreateProduct(ProductDto productDto)
    {
        var productAdd = new Product()
        {
            ProductId = productDto.ProductId,
            ProductName = productDto.ProductName,
            ProductPrice = productDto.ProductPrice
        };

        _orderdbContext.Products.Add(productAdd);
        _orderdbContext.SaveChanges();

        return productAdd;
    }
}