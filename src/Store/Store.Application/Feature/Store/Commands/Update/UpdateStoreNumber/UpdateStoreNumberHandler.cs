using AutoMapper;
using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Application.Feature.Store.Commands.Update.UpdateStoreNumber
{
    public class UpdateStoreNumberHandler : IRequestHandler<UpdateStoreNumberCommand, ResultDto>
    {
        private readonly IStoreRespository _storeResp;
        private readonly IMapper _mapper;
        public UpdateStoreNumberHandler(IStoreRespository storeResp, IMapper mapper)
        {
            _storeResp = storeResp;
            _mapper = mapper;
        }

        public async Task<ResultDto> Handle(UpdateStoreNumberCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<UpdateNumberDto>(request);
            var update = await _storeResp.UpdateInventoryAfterPurchase(map);

            return update;
        }
    }
}
