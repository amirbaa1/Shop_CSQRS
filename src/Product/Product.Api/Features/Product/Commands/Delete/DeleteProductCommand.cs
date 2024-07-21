using System.Diagnostics.SymbolStore;
using MediatR;

namespace Product.Api.Features.Product.Commands.Delete;

public class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}