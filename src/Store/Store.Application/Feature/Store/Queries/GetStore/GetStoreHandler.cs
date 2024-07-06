using AutoMapper;
using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;


namespace Store.Application.Feature.Store.Queries.GetStore
{
    public class GetStoreHandler : IRequestHandler<GetStoreQuery, List<StoreDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStoreRespository _storeRespository;

        public GetStoreHandler(IStoreRespository storeRespository, IMapper mapper)
        {
            _storeRespository = storeRespository;
            _mapper = mapper;
        }

        public async Task<List<StoreDto>> Handle(GetStoreQuery request, CancellationToken cancellationToken)
        {
            var getStore = await _storeRespository.GetStore();

            return getStore;
        }
    }
}
