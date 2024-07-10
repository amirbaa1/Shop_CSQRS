using Basket.Domain.Repository;
using MediatR;

namespace Basket.Application.Features.Basket.Queries.Message;

public class MessageResultHandler : IRequestHandler<MessageResultCommand, bool>
{
    private readonly IMessageRepository _messageRepository;

    public MessageResultHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<bool> Handle(MessageResultCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.MessageResultSet(request);
        return message;
    }
}