using MassTransit;
using MediatR;
using Product.Application.Mapping;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Application.Features.Product.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, string>
{
    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publish;
    public UpdateProductHandler(IProductRepository productRepository, IPublishEndpoint publish)
    {
        _productRepository = productRepository;
        _publish = publish;
    }

    public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updateProduct = ProductMapper.Mapper.Map<UpdateProductDto>(request);

        var updateToProduct = await _productRepository.UpdateProduct(updateProduct);

        return updateToProduct;
    }
}