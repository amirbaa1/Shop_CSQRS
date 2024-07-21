using MediatR;

namespace Product.Api.Features.Category.Commands.Create;

public class CreateCategoryCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}