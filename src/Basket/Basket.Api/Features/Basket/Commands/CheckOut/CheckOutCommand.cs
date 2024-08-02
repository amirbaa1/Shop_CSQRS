using Basket.Domain.Model.Dto;
using MediatR;

namespace Basket.Api.Features.Basket.Commands.CheckOut;

public class CheckOutCommand : CheckOutDto,IRequest<ResultDto>
{
    
}