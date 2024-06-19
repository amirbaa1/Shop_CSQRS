using MediatR;
using Product.Domain.Model.Dto;

namespace Product.Application.Features.Category.Queries.GetCategory;

public class GetCategoryQuery : IRequest<List<CategoryDto>>
{
    
}