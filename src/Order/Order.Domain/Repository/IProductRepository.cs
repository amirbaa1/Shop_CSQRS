using Order.Domain.Model;
using Order.Domain.Model.Dto;

namespace Order.Domain.Repository;

public interface IProductRepository
{
    Product GetProduct(ProductDto productDto);
}