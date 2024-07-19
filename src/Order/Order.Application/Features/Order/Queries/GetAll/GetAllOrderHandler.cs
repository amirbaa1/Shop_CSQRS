using MediatR;
using Order.Domain.Model.Dto;
using Order.Domain.Repository;

namespace Order.Application.Features.Order.Queries.GetAll
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, List<OrderModelDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderModelDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetAll();

            return result;
        }
    }
}
