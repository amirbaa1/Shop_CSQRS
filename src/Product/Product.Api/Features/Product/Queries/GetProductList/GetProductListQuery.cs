using MediatR;
using Product.Domain.Model.Dto;

namespace Product.Api.Features.Product.Queries.GetProductList;

public class GetProductListQuery : IRequest<List<ProductDto>>
{
    
}