using MediatR;
using Order.Domain.Model;
using Order.Domain.Model.Dto;

namespace Order.Api.Features.Order.Commands.Create;

public class CreateOrderCommand :AddOrderDto, IRequest<bool>
{
    
}