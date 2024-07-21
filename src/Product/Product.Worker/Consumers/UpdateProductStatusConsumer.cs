using AutoMapper;
using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Api.Features.Product.Commands.Update.UpdateProductStatus;
using Product.Domain.Model.Dto;


namespace Product.Worker.Consumers
{
    public class UpdateProductStatusConsumer : IConsumer<UpdateProductStatusEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductStatusConsumer> _logger;

        public UpdateProductStatusConsumer(IMediator mediator, IMapper mapper,
            ILogger<UpdateProductStatusConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UpdateProductStatusEvent> context)
        {
            var message = context.Message;
            //var updateMessage = new UpdateProductStatusCommand
            //{
            //    ProductId = message.ProductId,
            //    ProductsStatus = MapProductStatusEventToProductStatus(message.ProductStatusEvent),
            //    Number = message.Number,
            //};

            _logger.LogInformation($"---> Consumer  : {JsonConvert.SerializeObject(message)} ");


            var map = _mapper.Map<UpdateProductStatusDto>(message);

            _logger.LogInformation($"---> Consumer Update Statsu : {JsonConvert.SerializeObject(map)} ");


            //TODO:MAP
            await _mediator.Send(new ProductStatusCommand
            {
                ProductId = map.ProductId,
                ProductStatus = map.ProductStatus,
                Number = map.Number,
            });

            await Task.CompletedTask;
        }
    }
}