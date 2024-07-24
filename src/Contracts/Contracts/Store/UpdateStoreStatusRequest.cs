using Contracts.Product;

namespace Contracts.Store;

public class UpdateStoreStatusRequest
{
    public Guid ProductId { get; set; }
    public ProductStatusRequest ProductStatus { get; set; }
    public int Price { get; set; }
    public int Number { get; set; }
}