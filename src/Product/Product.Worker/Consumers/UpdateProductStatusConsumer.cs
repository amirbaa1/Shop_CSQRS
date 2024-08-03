using AutoMapper;
using Contracts.General;
using Contracts.Store;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Domain.Model;
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

            _logger.LogInformation($"---> Consumer  : {JsonConvert.SerializeObject(message)} ");


            var map = new UpdateProductStatusDto
            {
                ProductId = message.ProductId,
                ProductStatus = (ProductStatus)message.ProductStatus,
                Number = message.Number
            };


            var response = await _productRepository.UpdateProductStatus(map);


            var result = new ResponseResult();
            if (string.IsNullOrEmpty(response))
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