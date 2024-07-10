using AutoMapper;
using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Basket.Application.Features.Basket.Commands.Create
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketCommand, string>
    {
        private readonly IMapper _mapper;
        private IBasketRepository _repository;
        private readonly ILogger<CreateBasketCommand> _logger;
        public CreateBasketHandler(IMapper mapper, IBasketRepository repository, ILogger<CreateBasketCommand> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<string> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basketUser = await _repository.GetOrCreateBasketForUser(request.UserId);

            if (basketUser == null)
            {
                _logger.LogError("Basket creation failed for user {UserId}", request.UserId);
                throw new Exception("Basket creation failed");
            }

            if (request.ProductId == Guid.Empty || request.ProductId == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            {
                request.ProductId = Guid.NewGuid();
            }


            var itemBasket = new AddItemToBasketDto
            {
                BasketId = request.BasketId,
                Quantity = request.Quantity,
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                UnitPrice = request.UnitPrice,
                ImageUrl = request.ImageUrl,
            };
            
            var addBasket = await _repository.AddBasket(itemBasket);

            if (addBasket == null)
            {
                throw new Exception("Failed to add items to basket");
            }

            //var updatedBasket = await _repository.GetOrCreateBasketForUser(request.UserId);

            //var basketMap = _mapper.Map<BasketModelDto>(updatedBasket);

            //return basketMap; // outPrint 


            return addBasket;
        }
    }
}
