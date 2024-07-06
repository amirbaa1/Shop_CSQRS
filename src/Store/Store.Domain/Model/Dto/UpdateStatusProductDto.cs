namespace Store.Domain.Model.Dto;

public class UpdateStatusProductDto
{
    public Guid Id { get; set; }
    public ProductStatus ProductStatus { get; set; }
    public int Number { get; set; }
}