using Basket.Domain.Repository;
using MediatR;


namespace Basket.Api.Features.Basket.Commands.Delete
{
    public class DeleteBasketHandler : IRequestHandler<DeleteBasketCommand, string>
    {
        private readonly IBasketRepository _repository;

        public DeleteBasketHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basketDelete = await _repository.RemoveItemFromBasket(request.BasketId);

            return basketDelete;
        }
    }
}
