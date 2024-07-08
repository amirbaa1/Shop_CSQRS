using AutoMapper;
using EventBus.Messages.Event.Store;
using MassTransit;
using MediatR;
using Store.Application.Feature.Store.Queries.Check;

namespace Store.Infrastructure.Consumer;

public class CheckStoreConsumer : IConsumer<CheckStoreEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CheckStoreConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<CheckStoreEvent> context)
    {
        var map = _mapper.Map<CheckStoreQuery>(context);
        await _mediator.Send(map);
        await Task.CompletedTask;
    }
}