using AutoMapper;
using MediatR;
using Product.Domain.Repositories;

namespace Product.Api.Features.Product.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, List<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productById = await _productRepository.GetProductById(request.Id);
        
        var productMapper = _mapper.Map<List<ProductResponse>>(productById);

        return productMapper;
    }
}