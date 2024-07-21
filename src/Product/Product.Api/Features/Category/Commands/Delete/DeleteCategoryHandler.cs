using MediatR;
using Product.Domain.Repositories;

namespace Product.Api.Features.Category.Commands.Delete;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand,bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var deleteMapProduct =await _categoryRepository.DeleteCategory(request.Id);

        return  deleteMapProduct; 
    }
}