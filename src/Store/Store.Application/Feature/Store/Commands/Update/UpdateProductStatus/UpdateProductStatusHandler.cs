using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Application.Feature.Store.Commands.Update.UpdateProductStatus;

public class UpdateProductStatusHandler : IRequestHandler<UpdateProductStatusCommand, ResultDto>
{
    private readonly IStoreRepository _storeRepository;

    public UpdateProductStatusHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public Task<ResultDto> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
    {
        var update = _storeRepository.UpdateStatusProduct(request);

        return update;
    }
}