using MediatR;


namespace Basket.Api.Features.Basket.Commands.Update
{
    public class UpdateBasketCommand : IRequest<string>
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }

        public UpdateBasketCommand(Guid basketItemId, int quantity)
        {
            BasketItemId = basketItemId;
            Quantity = quantity;
        }
    }
}
