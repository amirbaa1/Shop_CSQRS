using MediatR;
using Product.Api.Mapping;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Api.Features.Category.Commands.Create;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand,string>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categpry = ProductMapper.Mapper.Map<CategoryDto>(request);

        var addToCate =await _categoryRepository.AddNewCategory(categpry);

        return addToCate;

    }
}