using MediatR;
using Product.Application.Mapping;
using Product.Domain.Repositories;

namespace Product.Application.Features.Category.Queries.GetCategory;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery,List<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async  Task<List<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var getAll =await _categoryRepository.GetCategories();

        var cateGet = ProductMapper.Mapper.Map<List<CategoryResponse>>(getAll);

        return cateGet;
    }
}