using Basket.Domain.Model.Dto;
using MediatR;

namespace Basket.Application.Features.Basket.Commands.CheckOut;

public class CheckOutCommand : CheckOutDto,IRequest<ResultDto>
{
    
}