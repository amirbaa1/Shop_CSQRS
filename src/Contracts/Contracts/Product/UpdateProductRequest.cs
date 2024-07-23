namespace Contracts.Product;

public class UpdateProductRequest
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductPrice { get; set; }
}