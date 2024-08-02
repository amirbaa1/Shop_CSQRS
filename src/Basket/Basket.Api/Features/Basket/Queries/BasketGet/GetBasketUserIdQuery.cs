using Basket.Domain.Model.Dto;
using MediatR;


namespace Basket.Api.Features.Basket.Queries.BasketGet
{
    public class GetBasketUserIdQuery : IRequest<BasketModelDto>
    {
        public string UserId { get; set; }

        public GetBasketUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
