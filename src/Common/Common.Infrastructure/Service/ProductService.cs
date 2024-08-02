using Contracts.General;
using Contracts.Product;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Service;

public class ProductService
{
    private readonly IRequestClient<ProductAddStoreRequest> _productAddStore;
    private readonly IRequestClient<UpdateProductRequest> _updateProduct;
    private readonly IRequestClient<ProductDeleteRequest> _productDelete;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IRequestClient<ProductAddStoreRequest> productAddStore, ILogger<ProductService> logger,
        IRequestClient<UpdateProductRequest> updateProduct, IRequestClient<ProductDeleteRequest> productDelete)
    {
        _productAddStore = productAddStore;
        _logger = logger;
        _updateProduct = updateProduct;
        _productDelete = productDelete;
    }

    public async Task<bool> PostProductStore(Guid productId, string productName, int number, int price,
        ProductStatusRequest productStatus)
    {
        var response = await _productAddStore.GetResponse<ResponseResult>(new ProductAddStoreRequest
        {
            ProductId = productId,
            ProductName = productName,
            Number = number,
            Price = price,
            ProductStatusEvent = productStatus
        });

        _logger.LogInformation($"---> response : {response.Message.Message}");

        if (response.Message.IsSuccessful == false)
        {
            return false;
        }

        return true;
    }

    public async Task<ResponseResult> ProductUpdate(Guid productId, string productName, int price)
    {
        var response = await _updateProduct.GetResponse<ResponseResult>(new UpdateProductRequest
        {
            ProductId = productId,
            ProductName = productName,
            ProductPrice = price
        });

        if (response.Message.IsSuccessful == false)
        {
            return new ResponseResult
            {
                IsSuccessful = false,
                Message = response.Message.Message
            };
        }

        return new ResponseResult
        {
            IsSuccessful = true,
            Message = response.Message.Message
        };
    }

    public async Task<ResponseResult> DeleteProduct(Guid productId)
    {
        var response = await _productDelete.GetResponse<ResponseResult>(new ProductDeleteRequest
        {
            productId = productId
        });
        if (response.Message.IsSuccessful == false)
        {
            return new ResponseResult
            {
                IsSuccessful = false,
                Message = response.Message.Message
            };
        }

        return new ResponseResult
        {
            IsSuccessful = true,
            Message = response.Message.Message
        };
    }
}