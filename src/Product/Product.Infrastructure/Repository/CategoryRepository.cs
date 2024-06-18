using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Domain.Model;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ProductDbContext _dbContext;
    private readonly ILogger<CategoryRepository> _logger;

    public CategoryRepository(ILogger<CategoryRepository> logger, ProductDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<List<CategoryDto>> GetCategories()
    {
        var getAllCategory = await _dbContext.Categories
            .OrderBy(x => x.Name)
            .Select(p => new CategoryDto
            {
                Description = p.Description,
                Name = p.Name,
                Id = p.Id
            }).ToListAsync();
        return getAllCategory;
    }

    public async Task<string> AddNewCategory(CategoryDto category)
    {
        var categoryCreate = new Category
        {
            Description = category.Description,
            Name = category.Name
        };

        _dbContext.Categories.Add(categoryCreate);
       await _dbContext.SaveChangesAsync();
       return "add to database";
    }
}