using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using EventBus.Messages.Event.Product;
using MassTransit;

namespace Basket.Infrastructure.Consumer;

public class UpdateProductConsumer : IConsumer<ProductQueueEvent>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task Consume(ConsumeContext<ProductQueueEvent> context)
    {
        var getMessage = context.Message;

        var product = new ProductDto
        {
            ProductId = getMessage.Id,
            ProductName = getMessage.Name,
            UnitPrice = getMessage.Price
        };

        var update = _productRepository.UpdateProduct(product.ProductId, product.ProductName, product.UnitPrice);

        if (update == false)
        {
            return Task.FromResult(false);
        }

        return Task.CompletedTask;
    }
}