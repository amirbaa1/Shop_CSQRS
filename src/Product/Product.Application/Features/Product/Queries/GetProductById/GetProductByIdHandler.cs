using MediatR;
using Product.Application.Mapping;
using Product.Domain.Repositories;

namespace Product.Application.Features.Product.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, List<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productById = await _productRepository.GetProductById(request.Id);
        
        var productMapper = ProductMapper.Mapper.Map<List<ProductResponse>>(productById);

        return productMapper;
    }
}