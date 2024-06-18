using AutoMapper;
using MediatR;
using Product.Application.Mapping;
using Product.Domain.Repositories;

namespace Product.Application.Features.Queries.GetProductList;

public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductListHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductResponse>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductList();

        var responseList = ProductMapper.Mapper.Map<List<ProductResponse>>(product);

        return responseList;
    }
}