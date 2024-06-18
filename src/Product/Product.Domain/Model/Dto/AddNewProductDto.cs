namespace Product.Domain.Model.Dto;

public class AddNewProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public Guid CategoryId { get; set; }
}