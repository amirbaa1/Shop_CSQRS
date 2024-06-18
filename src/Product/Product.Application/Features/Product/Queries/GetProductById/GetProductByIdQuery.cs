using MediatR;

namespace Product.Application.Features.Product.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<List<ProductResponse>>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}