using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Store.Application.Feature.Store.Commands.Update.UpdateProductName;

namespace Store.Infrastructure.Consumer;

public class UpdateProductStoreConsumer : IConsumer<ProductStoreUpdateEvent>
{
    private readonly IMediator _mediator;

    public UpdateProductStoreConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductStoreUpdateEvent> context)
    {
        var message = context.Message;
        var updateMessage = new UpdateProductNameCommand
        {
            Id = message.ProductId,
            Name = message.ProductName
        };

        await _mediator.Send(updateMessage);

        await Task.CompletedTask;
    }
}