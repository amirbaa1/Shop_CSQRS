using Basket.Domain.Repository;
using MediatR;


namespace Basket.Application.Features.Basket.Commands.Update
{
    internal class UpdateBasketHandler : IRequestHandler<UpdateBasketCommand, string>
    {
        private readonly IBasketRepository _repository;

        public UpdateBasketHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var updateBasket = await _repository.UpdateQuantities(request.BasketItemId,request.Quantity);

            return updateBasket;
        
        }
    }
}
