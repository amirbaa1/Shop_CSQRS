using AutoMapper;
using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using EventBus.Messages.Event.Store;
using MassTransit;

namespace Basket.Infrastructure.Consumer;

public class MessageResultConsumer : IConsumer<MessageCheckStoreEvent>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
    private readonly IBasketRepository _basketRepository;
    public MessageResultConsumer(IMessageRepository messageRepository, IMapper mapper, IBasketRepository basketRepository)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
        _basketRepository = basketRepository;
    }

    public async Task Consume(ConsumeContext<MessageCheckStoreEvent> context)
    {
        var map = _mapper.Map<ResultDto>(context.Message);

        _messageRepository.MessageResult(map);

        await Task.CompletedTask;
    }
}