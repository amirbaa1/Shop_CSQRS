using MediatR;
using Product.Application.Mapping;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Application.Features.Category.Queries.GetCategory;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery,List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async  Task<List<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var getAll =await _categoryRepository.GetCategories();

        var cateGet = ProductMapper.Mapper.Map<List<CategoryDto>>(getAll);

        return cateGet;
    }
}