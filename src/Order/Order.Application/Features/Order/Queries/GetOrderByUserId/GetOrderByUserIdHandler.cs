using AutoMapper;
using MediatR;
using Order.Domain.Model.Dto;
using Order.Domain.Repository;

namespace Order.Application.Features.Order.Queries.GetOrderByUserId;

public class GetOrderByUserIdHandler : IRequestHandler<GetOrderByUserIdQuery, List<OrderModelDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByUserIdHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderModelDto>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrdersByUserId(request.UserId);
        var map = _mapper.Map<List<OrderModelDto>>(order);
        return map;
    }
}