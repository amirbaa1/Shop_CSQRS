using Basket.Domain.Model.Dto;
using MediatR;
using Newtonsoft.Json;


namespace Basket.Application.Features.Basket.Commands.Create
{
    public class CreateBasketCommand : IRequest<BasketModelDto>
    {
        public string UserId { get; set; }
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}