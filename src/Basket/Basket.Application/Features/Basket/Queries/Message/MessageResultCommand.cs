using Basket.Domain.Model.Dto;
using MediatR;

namespace Basket.Application.Features.Basket.Queries.Message;

public class MessageResultCommand : ResultDto, IRequest<bool>
{
}