namespace Order.Domain.Model.Dto;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductPrice { get; set; }
    // public List<OrderLine> orderLines { get; set; }
}