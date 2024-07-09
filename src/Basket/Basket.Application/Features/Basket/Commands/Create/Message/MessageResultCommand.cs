using Basket.Domain.Model.Dto;
using MediatR;

namespace Basket.Application.Features.Basket.Commands.Create.Message;

public class MessageResultCommand : ResultDto, IRequest<bool>
{
}