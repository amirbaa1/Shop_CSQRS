using MediatR;
using Product.Application.Features.Product.Commands.CreateProduct;
using Product.Application.Mapping;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Application.Features.Category.Commands;

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