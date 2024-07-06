using AutoMapper;
using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;


namespace Store.Application.Feature.Store.Commands.Create
{
    internal class CreateStoreHandler : IRequestHandler<CreateStoreCommand, ResultDto>
    {
        private readonly IStoreRespository _storeRes;
        private readonly IMapper _mapper;
        public CreateStoreHandler(IStoreRespository storeRes, IMapper mapper)
        {
            _storeRes = storeRes;
            _mapper = mapper;
        }

        public async Task<ResultDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var reqMap = _mapper.Map<CreateStoreCommand>(request);
            var createStore = await _storeRes.CreateStore(reqMap);
            return createStore;
        }
    }
}
