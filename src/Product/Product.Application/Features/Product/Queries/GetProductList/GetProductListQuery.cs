using MediatR;
using Product.Domain.Model.Dto;

namespace Product.Application.Features.Queries.GetProductList;

public class GetProductListQuery : IRequest<List<ProductResponse>>
{
    
}