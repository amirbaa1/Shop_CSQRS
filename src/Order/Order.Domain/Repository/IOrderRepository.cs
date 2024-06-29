using Order.Domain.Model.Dto;

namespace Order.Domain.Repository;

public interface IOrderRepository
{
    Task<List<OrderModelDto>> GetOrdersByUserId(string userid);

    Task<OrderModelDto> GetOrderById(Guid id);

    // Task<List<OrderModelDto>> GetAll();
    bool CreateOrder(OrderModelDto orderModelDtos);
}