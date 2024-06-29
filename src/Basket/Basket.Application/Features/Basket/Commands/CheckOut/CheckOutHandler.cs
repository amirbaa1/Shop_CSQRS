using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using MediatR;

namespace Basket.Application.Features.Basket.Commands.CheckOut;

public class CheckOutHandler : IRequestHandler<CheckOutCommand, ResultDto>
{
    private readonly IBasketRepository _basketRepository;
    
    public CheckOutHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ResultDto> Handle(CheckOutCommand request, CancellationToken cancellationToken)
    {
        var checkOut = await _basketRepository.CheckOutBasket(request);
        
        return checkOut;
    }
}