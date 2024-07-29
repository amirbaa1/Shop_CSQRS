using System.Net;
using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using Contracts.General;
using Contracts.Product;
using EventBus.Messages.Event.Product;
using MassTransit;

namespace Basket.Worker.Consumer;

public class UpdateProductConsumer : IConsumer<UpdateProductRequest>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<UpdateProductRequest> context)
    {
        var getMessage = context.Message;

        var product = new ProductDto
        {
            ProductId = getMessage.ProductId,
            ProductName = getMessage.ProductName,
            UnitPrice = getMessage.ProductPrice
        };

        var update = _productRepository.UpdateProduct(product.ProductId, product.ProductName, product.UnitPrice);

        var result = new ResponseResult();
        if (update == false)
        {
            result.IsSuccessful = false;
            result.Message = $"update problem";
            result.StatusCode = HttpStatusCode.BadRequest;
        }

        result.IsSuccessful = true;
        result.Message = $"update basket";
        result.StatusCode = HttpStatusCode.OK;

        await context.RespondAsync(result);
    }
}