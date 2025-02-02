using Contracts.General;
using MediatR;
using Product.Domain.Repositories;

namespace Product.Api.Features.Product.Commands.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand,bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var deleteMapProduct =await _productRepository.DeleteProduct(request.Id);

        return  deleteMapProduct;
    }
}