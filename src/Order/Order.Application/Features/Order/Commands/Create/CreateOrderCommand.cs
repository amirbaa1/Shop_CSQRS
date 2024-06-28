using MediatR;
using Order.Domain.Model;
using Order.Domain.Model.Dto;

namespace Order.Application.Features.Order.Commands.Create;

public class CreateOrderCommand :AddOrderDto, IRequest<bool>
{
}