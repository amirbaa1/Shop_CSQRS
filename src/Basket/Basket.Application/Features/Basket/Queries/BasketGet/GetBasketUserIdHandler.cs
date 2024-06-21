using AutoMapper;
using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;
using MediatR;

namespace Basket.Application.Features.Basket.Queries.BasketGet
{
    public class GetBasketUserIdHandler : IRequestHandler<GetBasketUserIdQuery, BasketModelDto>
    {
        private IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public GetBasketUserIdHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<BasketModelDto> Handle(GetBasketUserIdQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetOrCreateBasketForUser(request.UserId);

            var basketDtoMap = _mapper.Map<BasketModelDto>(basket);

            return basketDtoMap;

        }
    }
}

