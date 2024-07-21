using MediatR;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Api.Features.Product.Commands.Update.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, string>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // var updateProduct = ProductMapper.Mapper.Map<UpdateProductDto>(request);

        var updateToProduct = await _productRepository.UpdateProduct(request);

        return updateToProduct;
    }
}