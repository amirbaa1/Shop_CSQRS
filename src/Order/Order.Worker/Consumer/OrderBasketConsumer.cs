using System.Net;
using Contracts.Basket;
using Contracts.General;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order.Domain.Model.Dto;
using Order.Domain.Repository;

namespace Order.Worker.Consumer
{
    public class OrderBasketConsumer : IConsumer<SendToOrderRequest>
    {
        private readonly ILogger<OrderBasketConsumer> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderBasketConsumer(ILogger<OrderBasketConsumer> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<SendToOrderRequest> context)
        {
            var getMessage = context.Message;

            _logger.LogInformation($"Message basket to order ---> {JsonConvert.SerializeObject(getMessage)}");

            var orderModelMessage = new OrderModelDto
            {
                Id = getMessage.BasketId, //basketId or create Id?
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
                    ProductId = x.ProductId,
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

            var response = _orderRepository.CreateOrder(orderModelMessage);

            var result = new ResponseResult();
            if (response == false)
            {
                result.IsSuccessful = false;
                result.Message = $"problem create order";
                result.StatusCode = HttpStatusCode.BadRequest;
            }

            result.IsSuccessful = true;
            result.Message = $"crete order";
            result.StatusCode = HttpStatusCode.OK;

            await context.RespondAsync(result);
        }
    }
}