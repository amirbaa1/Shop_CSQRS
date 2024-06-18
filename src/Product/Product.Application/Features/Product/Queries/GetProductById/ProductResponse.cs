namespace Product.Application.Features.Product.Queries.GetProductById;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryResponse Category { get; set; }
}

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}