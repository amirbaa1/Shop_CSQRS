using AutoMapper;
using Contracts.General;
using Contracts.Store;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Api.Features.Product.Commands.Update.UpdateProductStatus;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;


namespace Product.Worker.Consumers
{
    // public class UpdateProductStatusConsumer : IConsumer<UpdateProductStatusEvent>
    public class UpdateProductStatusConsumer : IConsumer<UpdateStoreStatusRequest>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductStatusConsumer> _logger;
        private readonly IProductRepository _productRepository;

        public UpdateProductStatusConsumer(IMediator mediator, IMapper mapper,
            ILogger<UpdateProductStatusConsumer> logger, IProductRepository productRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
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
            //
            // _logger.LogInformation($"---> Consumer Update Statsu : {JsonConvert.SerializeObject(map)} ");
            //
            //
            // 
            // var productStatus = await _mediator.Send(new ProductStatusCommand
            // {
            //     ProductId = map.ProductId,
            //     ProductStatus = map.ProductStatus,
            //     Number = map.Number,
            // });

            var response = await _productRepository.UpdateProductStatus(map);


            var result = new ResponseResult();
            if (response == null)
            {
                result.IsSuccessful = false;
                result.Message = $"error :{response}";
                result.StatusCode = result.StatusCode;
            }

            result.IsSuccessful = true;
            result.Message = $"OK : {response}";
            result.StatusCode = result.StatusCode;
            
            await context.RespondAsync(result);
        }
    }
}