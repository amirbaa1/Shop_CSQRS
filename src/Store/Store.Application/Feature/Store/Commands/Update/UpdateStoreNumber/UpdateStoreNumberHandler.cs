using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Application.Feature.Store.Commands.Update.UpdateStoreNumber
{
    public class UpdateStoreNumberHandler : IRequestHandler<UpdateStoreNumberCommand, ResultDto>
    {
        private readonly IStoreRespository _storeResp;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStoreNumberHandler> _logger;
        public UpdateStoreNumberHandler(IStoreRespository storeResp, IMapper mapper, ILogger<UpdateStoreNumberHandler> logger)
        {
            _storeResp = storeResp;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultDto> Handle(UpdateStoreNumberCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<UpdateNumberDto>(request);
            // _logger.LogInformation($"--->Handler : {JsonConvert}");
            var update = await _storeResp.UpdateInventoryAfterPurchase(map);

            return update;
        }
    }
}
