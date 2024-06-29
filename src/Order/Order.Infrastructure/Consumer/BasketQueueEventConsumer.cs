
using EventBus.Messages.Event;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order.Domain.Model;
using Order.Domain.Model.Dto;
using Order.Domain.Repository;

namespace Order.Infrastructure.Consumer
{
    public class BasketQueueEventConsumer : IConsumer<BasketQueueEvent>            
    {
        private readonly ILogger<BasketQueueEventConsumer> _logger;
        private readonly IOrderRepository _orderRepository;

        public BasketQueueEventConsumer(ILogger<BasketQueueEventConsumer> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public Task Consume(ConsumeContext<BasketQueueEvent> context)
        {
            var getMessage = context.Message;

            _logger.LogInformation($"Message basket to order ---> {JsonConvert.SerializeObject(getMessage)}");

            var orderModelMessage = new OrderModelDto
            {
                Id = getMessage.Id,
                FirstName = getMessage.FirstName,
                LastName = getMessage.LastName,
                UserId = getMessage.UserId,
                Address = getMessage.Address,
                ZipCode = getMessage.ZipCode,
                EmailAddress = getMessage.EmailAddress,
                PhoneNumber = getMessage.PhoneNumber,
                TotalPrice = getMessage.TotalPrice,
                OrderLines = getMessage.BasketItems.Select(x => new OrderLineDto
                {
                    Id = x.BasketItemId,
                    ProductId =x.ProductId,
                    Quantity = x.Quantity,
                    Product = new ProductDto()
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Name,
                        ProductPrice = x.Price,
                    },
                    Total = x.Total,
                }).ToList(),
            };

            _logger.LogInformation($"---> orderModelMessage {JsonConvert.SerializeObject(orderModelMessage)}");

            _orderRepository.CreateOrder(orderModelMessage);
            return Task.CompletedTask;
        }
    }
}
