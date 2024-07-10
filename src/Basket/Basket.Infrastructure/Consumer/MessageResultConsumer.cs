using AutoMapper;
using Basket.Application.Features.Basket.Queries.Message;
using Basket.Domain.Repository;
using EventBus.Messages.Event.Store;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Consumer;

public class MessageResultConsumer : IConsumer<MessageCheckStoreEvent>
{
    private readonly IMapper _mapper;
    private readonly IBasketRepository _basketRepository;
    private readonly ILogger<MessageResultConsumer> _logger;
    private readonly IMediator _mediator;

    public MessageResultConsumer(IMapper mapper, IBasketRepository basketRepository,
        ILogger<MessageResultConsumer> logger, IMediator mediator)
    {
        _mapper = mapper;
        _basketRepository = basketRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<MessageCheckStoreEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"---->message :  {JsonConvert.SerializeObject(message)}");

        var map = _mapper.Map<MessageResultCommand>(message);
        _logger.LogInformation($"---->message :  {map}");

       await _mediator.Send(map);

        await Task.CompletedTask;
    }
}