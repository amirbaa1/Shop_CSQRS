using MediatR;
using Product.Application.Features.Queries.GetProductList;

namespace Product.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public Guid CategoryId { get; set; }
}