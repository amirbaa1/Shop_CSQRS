using EventBus.Messages.Event.Product;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;
    private readonly ILogger<ProductDbContext> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBus _bus;
    public ProductRepository(ProductDbContext context, ILogger<ProductDbContext> logger,
        IPublishEndpoint publishEndpoint, IBus bus)
    {
        _context = context;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _bus = bus;
    }

    public async Task<string> AddProduct(ProductDto addNewProductDto)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == addNewProductDto.CategoryId);

        if (category == null)
        {
            return "Not found category";
        }

        var productCreate = new Domain.Model.Product
        {
            Name = addNewProductDto.Name,
            Description = addNewProductDto.Description,
            Image = addNewProductDto.Image,
            Price = addNewProductDto.Price,
            CategoryId = addNewProductDto.CategoryId,
            Category = category,
            CreationDateTime = DateTime.UtcNow,
        };
        if (productCreate == null)
        {
            return "No add in database";
        }

        _context.Add(productCreate);

        await _context.SaveChangesAsync();

        return "add in database.";
    }

    public Task<List<ProductDto>> GetProductList()
    {
        var data = _context.Products
            .OrderByDescending(p => p.Id)
            .Select(p => new ProductDto
            {
                Description = p.Description,
                Id = p.Id,
                Image = p.Image,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Category = p.Category
            }).ToList();

        return Task.FromResult(data);
    }

    public async Task<ProductDto> GetProductById(Guid productId)
    {
        try
        {
            var productGetId = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var productGet = new ProductDto
            {
                Description = productGetId.Description,
                Name = productGetId.Name,
                Price = productGetId.Price,
                Id = productGetId.Id,
                Image = productGetId.Image,
                // Category = productGetId.Category,
                CategoryId = productGetId.CategoryId,
            };
            return productGet;
        }
        catch (Exception e)
        {
            _logger.LogError($"error message : {e.Message}");
            throw;
        }
    }

    public async Task<string> UpdateProduct(UpdateProductDto updateProduct)
    {
        var getProduct = await _context.Products.FindAsync(updateProduct.ProductId);
        if (getProduct == null)
        {
            return "Not found product ID.";
        }

        if (updateProduct.Name == "string" || string.IsNullOrWhiteSpace(updateProduct.Name))
        {
            getProduct.Name = getProduct.Name;
        }
        else
        {
            getProduct.Name = updateProduct.Name;
        }

        if (updateProduct.Price == 0)
        {
            getProduct.Price = getProduct.Price;
        }
        else
        {
            getProduct.Price = updateProduct.Price;
        }

        await _context.SaveChangesAsync();

        var message = new ProductQueueEvent
        {
            ProductId = getProduct.Id,
            Name = getProduct.Name,
            Price = getProduct.Price
        };

        await _publishEndpoint.Publish(message);
        // await _bus.Publish(message);
        return $"Update Product : {getProduct}";
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var getProduct = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
        if (getProduct == null)
        {
            return false;
        }

        _context.Products.Remove(getProduct);
        await _context.SaveChangesAsync();
        return true;
    }
}