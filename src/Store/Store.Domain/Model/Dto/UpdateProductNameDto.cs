namespace Store.Domain.Model.Dto;

public class UpdateProductNameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}