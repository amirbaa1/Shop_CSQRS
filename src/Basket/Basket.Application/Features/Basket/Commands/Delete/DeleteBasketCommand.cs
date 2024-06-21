using MediatR;

namespace Basket.Application.Features.Basket.Commands.Delete
{
    public class DeleteBasketCommand : IRequest<string>
    {
        public Guid BasketId { get; set; }

        public DeleteBasketCommand(Guid basketId)
        {

            BasketId = basketId;
        }

    }
}
