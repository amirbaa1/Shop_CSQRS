using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order.Domain.Model.Dto;
using Order.Domain.Repository;

namespace Order.Application.Features.Order.Commands.Create;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(IMapper mapper, IOrderRepository orderRepository, ILogger<CreateOrderHandler> logger)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderMap =  _mapper.Map<OrderModelDto>(request);
        
        _logger.LogInformation($"ordermap : --->{JsonConvert.SerializeObject(orderMap)}");
        var createOrder =  _orderRepository.CreateOrder(orderMap);
        return Task.FromResult(createOrder.Id);
    }
}