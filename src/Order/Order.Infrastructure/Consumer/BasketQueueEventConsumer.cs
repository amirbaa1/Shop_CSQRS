
using EventBus.Messages.Event;
using MassTransit;
using Microsoft.Extensions.Logging;
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

            _logger.LogInformation($"Message basket to order ---> {getMessage}");

            return Task.CompletedTask;
        }
    }
}
