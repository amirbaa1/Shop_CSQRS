using Product.Domain.Model;

namespace Contracts.Product;

public class ProductAddStoreRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public ProductStatus ProductStatusEvent { get; set; }
    public int Number { get; set; }
}