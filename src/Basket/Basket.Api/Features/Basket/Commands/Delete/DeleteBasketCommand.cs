using MediatR;

namespace Basket.Api.Features.Basket.Commands.Delete
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
