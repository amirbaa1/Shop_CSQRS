using AutoMapper;
using EventBus.Messages.Event.Product;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Application.Features.Product.Commands.Update.UpdateProductStatus;
using Product.Domain.Model.Dto;


namespace Product.Infrastructure.Consumser
{
    public class UpdateProductStatusConsumer : IConsumer<UpdateProductStatusEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductStatusConsumer> _logger;
        public UpdateProductStatusConsumer(IMediator mediator, IMapper mapper, ILogger<UpdateProductStatusConsumer> logger)
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

        //private ProductStatus MapProductStatusEventToStoreProductStatus(ProductStatusEvent productStatusEvent)
        //{
        //    var productStatus = productStatusEvent switch
        //    {
        //        ProductStatusEvent.Available => Product.Domain.Model.ProductStatus.Available,
        //        ProductStatusEvent.OutOfStock => Product.Domain.Model.ProductStatus.OutOfStock,
        //        _ => throw new ArgumentOutOfRangeException(nameof(productStatusEvent), productStatusEvent, null)
        //    };

        //    return productStatus switch
        //    {
        //        Product.Domain.Model.ProductStatus.Available => ProductStatus.Available,
        //        Product.Domain.Model.ProductStatus.OutOfStock =>ProductStatus.OutOfStock,
        //        _ => throw new ArgumentOutOfRangeException(nameof(productStatus), productStatus, null)
        //    };
        //}
    }

}
