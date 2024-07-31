using AutoMapper;
using MediatR;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Api.Features.Category.Queries.GetCategory;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery,List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async  Task<List<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var getAll =await _categoryRepository.GetCategories();

        var cateGet = _mapper.Map<List<CategoryDto>>(getAll);

        return cateGet;
    }
}