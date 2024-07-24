using AutoMapper;
using Contracts.Store;
using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Api.Features.Product.Commands.Update.UpdateProductStatus;
using Product.Domain.Model.Dto;
using ResponseResult = Contracts.General.ResponseResult;


namespace Product.Worker.Consumers
{
    // public class UpdateProductStatusConsumer : IConsumer<UpdateProductStatusEvent>
    public class UpdateProductStatusConsumer : IConsumer<UpdateStoreStatusRequest>
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

        public async Task Consume(ConsumeContext<UpdateStoreStatusRequest> context)
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
            var productStatus = await _mediator.Send(new ProductStatusCommand
            {
                ProductId = map.ProductId,
                ProductStatus = map.ProductStatus,
                Number = map.Number,
            });

            var result = new ResponseResult();
            if (productStatus == null)
            {
                result.Isuccess = false;
                result.Message = $"{productStatus}";
            }

            result.Isuccess = true;
            result.Message = "Update store";


            await context.RespondAsync(result);
        }
    }
}