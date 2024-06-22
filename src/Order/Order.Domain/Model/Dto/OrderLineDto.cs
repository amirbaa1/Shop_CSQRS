namespace Order.Domain.Model.Dto;

public class OrderLineDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
    public int Quantity { get; set; }
    public int Total { get; set; }
}