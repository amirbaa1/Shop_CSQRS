using MediatR;
using Order.Domain.Model.Dto;

namespace Order.Api.Features.Order.Queries.GetOrderByUserId;

public class GetOrderByUserIdQuery : IRequest<List<OrderModelDto>>
{
    public string UserId { get; set; }

    public GetOrderByUserIdQuery(string userId)
    {
        UserId = userId;
    }
}