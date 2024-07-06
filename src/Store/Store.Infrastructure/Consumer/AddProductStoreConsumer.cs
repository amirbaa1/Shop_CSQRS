using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Application.Feature.Store.Commands.Create;
using Store.Domain.Model;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Infrastructure.Consumer;

public class AddProductStoreConsumer : IConsumer<ProductStoreEvent>
{
    private readonly IStoreRespository _storeRespository;
    private readonly ILogger<AddProductStoreConsumer> _logger;
    private readonly IMediator _mediator;

    public AddProductStoreConsumer(IStoreRespository storeRespository, ILogger<AddProductStoreConsumer> logger,
        IMediator mediator)
    {
        _storeRespository = storeRespository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductStoreEvent> context)
    {
        var message = context.Message;


        var storeMessage = new CreateStoreCommand
        {
            ProductId = message.ProductId,
            ProductName = message.ProductName,
            Price = message.Price,
            Number = message.Number,
            Status = (ProductStatus)(int)message.ProductStatusEvent
        };
        _logger.LogInformation($"---> consumer : {JsonConvert.SerializeObject(storeMessage)}");


        // _storeRespository.CreateStore(storeMessage);
        await _mediator.Send(storeMessage);

        await Task.CompletedTask; 
    }
}