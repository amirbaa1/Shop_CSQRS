using Contracts.General;
using Contracts.Product;
using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Newtonsoft.Json;
using Store.Api.Feature.Store.Commands.Update.UpdateProductName;

namespace Store.Api.Consumer;

public class UpdateProductStoreConsumer : IConsumer<UpdateProductRequest>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UpdateProductStoreConsumer> _logger;

    public UpdateProductStoreConsumer(IMediator mediator, ILogger<UpdateProductStoreConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UpdateProductRequest> context)
    {
        var message = context.Message;
        var updateMessage = new UpdateProductNameCommand
        {
            Id = message.ProductId,
            Name = message.ProductName,
            Price = message.ProductPrice,
        };

        _logger.LogInformation($"--->{JsonConvert.SerializeObject(updateMessage)}");

        await _mediator.Send(updateMessage);

        var result = new ResponseResult
        {
            Isuccess = true,
            Message = "Update product in store"
        };

        await context.RespondAsync(result);
    }
}