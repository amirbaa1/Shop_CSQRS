using Basket.Domain.Model.Dto;
using MediatR;
using Newtonsoft.Json;


namespace Basket.Application.Features.Basket.Commands.Create
{
    public class CreateBasketCommand : AddItemToBasketDto , IRequest<string>
    {
        public string UserId { get; set; }

        public CreateBasketCommand(string userId)
        {
            UserId = userId;
        }
    }
}