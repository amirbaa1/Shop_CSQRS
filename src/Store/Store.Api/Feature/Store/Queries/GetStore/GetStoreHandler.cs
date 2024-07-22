using AutoMapper;
using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;


namespace Store.Api.Feature.Store.Queries.GetStore
{
    public class GetStoreHandler : IRequestHandler<GetStoreQuery, List<StoreDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;

        public GetStoreHandler(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<List<StoreDto>> Handle(GetStoreQuery request, CancellationToken cancellationToken)
        {
            var getStore = await _storeRepository.GetStore();

            return getStore;
        }
    }
}
