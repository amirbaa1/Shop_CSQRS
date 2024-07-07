using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;


namespace Product.Application.Features.Product.Commands.Update.UpdateProductStatus
{
    public class ProductStatusHandler : IRequestHandler<ProductStatusCommand, string>
    {
        private readonly IProductRepository _productRes;
        private readonly ILogger<ProductStatusHandler> _logger;
        private readonly IMapper _mapper;
        public ProductStatusHandler(ILogger<ProductStatusHandler> logger, IMapper mapper, IProductRepository productRes)
        {
            _logger = logger;
            _mapper = mapper;
            _productRes = productRes;
        }

        public Task<string> Handle(ProductStatusCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<UpdateProductStatusDto>(request);

            _logger.LogInformation($"----> Handler : {JsonConvert.SerializeObject(map)} ");

            var command = _productRes.UpdateProductStatus(map);

            return command;

        }
    }
}
