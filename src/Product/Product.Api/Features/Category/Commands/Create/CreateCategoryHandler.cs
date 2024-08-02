using AutoMapper;
using MediatR;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Api.Features.Category.Commands.Create;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, string>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categpry = _mapper.Map<CategoryDto>(request);

        var addToCate = await _categoryRepository.AddNewCategory(categpry);

        return addToCate;
    }
}