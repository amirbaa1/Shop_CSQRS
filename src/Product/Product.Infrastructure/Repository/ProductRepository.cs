using Common.Infrastructure.Service;
using Contracts.General;
using Contracts.Product;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;
    private readonly ILogger<ProductDbContext> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ProductService _productService;

    public ProductRepository(ProductDbContext context, ILogger<ProductDbContext> logger,
        IPublishEndpoint publishEndpoint, ProductService productService)
    {
        _context = context;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _productService = productService;
    }

    public async Task<string> AddProduct(ProductDto addNewProduct)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == addNewProduct.CategoryId);

        if (category == null)
        {
            return "Not found category";
        }

        var productCreate = new Domain.Model.Product
        {
            Name = addNewProduct.Name,
            Description = addNewProduct.Description,
            Image = addNewProduct.Image,
            ProductStatus = addNewProduct.ProductStatus,
            Number = addNewProduct.Number,
            Price = addNewProduct.Price,
            CategoryId = addNewProduct.CategoryId,
            Category = category,
            CreationDateTime = DateTime.UtcNow,
        };
        if (productCreate == null)
        {
            return "No add in database";
        }

        _context.Add(productCreate);

        var store = await _productService.PostProductStore(productCreate.Id, productCreate.Name, productCreate.Number,
            productCreate.Price, (ProductStatusRequest)productCreate.ProductStatus);

        if (store == true)
        {
            await _context.SaveChangesAsync();

            return "add in database";
        }

        // var message = new ProductStoreEvent
        // {
        //     ProductId = productCreate.Id,
        //     ProductName = productCreate.Name,
        //     Number = productCreate.Number,
        //     Price = productCreate.Price,
        //     ProductStatusEvent = (ProductStatusEvent)(int)productCreate.ProductStatus,
        // };
        // await _publishEndpoint.Publish(message);


        return "error in store";
        // return "add in database.";
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
                ProductStatus = p.ProductStatus,
                Number = p.Number,
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
        _logger.LogInformation($"---> repository Updaproduct? : {JsonConvert.SerializeObject(updateProduct)}");
        var getProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == updateProduct.ProductId);
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


        _context.Products.Update(getProduct);

        await _context.SaveChangesAsync();


        // var message = new ProductQueueEvent //update basket for updare product name or product price
        // {
        //     ProductId = getProduct.Id,
        //     Name = getProduct.Name,
        //     Price = getProduct.Price
        // };


        // var messageUpdateStore = new ProductStoreUpdateEvent // update store for updare product name or product price
        // {
        //     ProductId = getProduct.Id,
        //     ProductName = getProduct.Name,
        //     ProductPrice = getProduct.Price
        // };

        var response = await _productService.ProductUpdate(getProduct.Id, getProduct.Name, getProduct.Price);


        _logger.LogInformation($"--->{JsonConvert.SerializeObject(response)}");

        // await _publishEndpoint.Publish(message);
        // await _publishEndpoint.Publish(messageUpdateStore);

        if (response.IsSuccessful == false)
        {
            return $"error : {response}";
        }

        return $"response : {response} //Update Product : {JsonConvert.SerializeObject(getProduct)}";
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var getProduct = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);
        if (getProduct == null)
        {
            // return new ResponseResult
            // {
            //     Isuccess = false,
            //     Message = "Not found Product"
            // };
            return false;
        }

        _context.Products.Remove(getProduct);

        var result = await _productService.DeleteProduct(productId);
        if (result.IsSuccessful == false)
        {
            // return new ResponseResult
            // {
            //     Isuccess = false,
            //     Message = result.Message
            // };
            return false;
        }

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<string> UpdateProductStatus(UpdateProductStatusDto updateProductStatusDto)
    {
        _logger.LogInformation($"---> repository UpdateSta? : {JsonConvert.SerializeObject(updateProductStatusDto)}");

        var getProduct = await _context.Products.SingleOrDefaultAsync(x => x.Id == updateProductStatusDto.ProductId);
        if (getProduct == null)
        {
            _logger.LogError("Not Found product");
            return "Not Found product";
        }

        _logger.LogInformation($"Get Product --->{JsonConvert.SerializeObject(getProduct)}");
        getProduct.ProductStatus = updateProductStatusDto.ProductStatus;
        getProduct.Number = updateProductStatusDto.Number;
        _logger.LogInformation($"Update --->{JsonConvert.SerializeObject(getProduct)}");

        _context.Products.Update(getProduct);

        await _context.SaveChangesAsync();

        return "Update status product";
    }
}