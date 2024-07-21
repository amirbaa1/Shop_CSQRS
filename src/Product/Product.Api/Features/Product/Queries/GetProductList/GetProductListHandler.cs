using AutoMapper;
using MediatR;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;
using Product.Api.Mapping;

namespace Product.Api.Features.Product.Queries.GetProductList;

public class GetProductListHandler : IRequestHandler<GetProductListQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductListHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductList();

        var responseList = ProductMapper.Mapper.Map<List<ProductDto>>(product);

        return responseList;
    }
}