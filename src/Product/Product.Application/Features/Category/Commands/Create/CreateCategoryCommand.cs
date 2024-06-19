using MediatR;

namespace Product.Application.Features.Category.Commands.Create;

public class CreateCategoryCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}