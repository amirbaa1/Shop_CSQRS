using Product.Domain.Model.Dto;

namespace Product.Domain.Repositories;

public interface ICategoryRepository
{
   Task<List<CategoryDto>> GetCategories();
    Task<string> AddNewCategory(CategoryDto category);
}