namespace Store.Domain.Model.Dto;

public class UpdateProductNameDto
{
    public Guid productId { get; set; }
    public string productName { get; set; }
    public int productPrice { get; set; }
}