using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Api.Feature.Store.Queries.Check;

public class CheckStoreHandler : IRequestHandler<CheckStoreQuery, ResultDto>
{
    private readonly IStoreRepository _storeRepository;

    public CheckStoreHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<ResultDto> Handle(CheckStoreQuery request, CancellationToken cancellationToken)
    {
        var result = await _storeRepository.CheckStore(request);
        return result;
    }
}