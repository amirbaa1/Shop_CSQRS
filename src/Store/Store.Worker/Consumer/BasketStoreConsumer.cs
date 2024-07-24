using AutoMapper;
using EventBus.Messages.Event.Basket;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Api.Feature.Store.Commands.Update.UpdateStoreNumber;

namespace Store.Worker.Consumer;

public class BasketStoreConsumer : IConsumer<BasketStoreEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketStoreConsumer> _logger;

    public BasketStoreConsumer(IMediator mediator, IMapper mapper, ILogger<BasketStoreConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketStoreEvent> context)
    {
        var message = context.Message;

        var mapReq = _mapper.Map<UpdateStoreNumberCommand>(message);
        _logger.LogInformation($"---> consumer basket :{JsonConvert.SerializeObject(mapReq)}");
        await _mediator.Send(mapReq);
        await Task.CompletedTask;
    }
}