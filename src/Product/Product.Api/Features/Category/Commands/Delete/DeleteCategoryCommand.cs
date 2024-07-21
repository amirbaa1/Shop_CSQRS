using MediatR;

namespace Product.Api.Features.Category.Commands.Delete;

public class DeleteCategoryCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}