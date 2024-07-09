using AutoMapper;
using EventBus.Messages.Event.Store;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Store.Application.Feature.Store.Queries.Check;

namespace Store.Infrastructure.Consumer;

public class CheckStoreConsumer : IConsumer<CheckStoreEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CheckStoreConsumer> _logger;
    public CheckStoreConsumer(IMediator mediator, IMapper mapper, ILogger<CheckStoreConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CheckStoreEvent> context)
    {
        var map = _mapper.Map<CheckStoreQuery>(context.Message);
        _logger.LogInformation($"---> {map}");
        await _mediator.Send(map);
        await Task.CompletedTask;
    }
}