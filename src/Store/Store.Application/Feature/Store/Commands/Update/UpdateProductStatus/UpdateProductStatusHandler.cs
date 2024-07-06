using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Application.Feature.Store.Commands.Update.UpdateProductStatus;

public class UpdateProductStatusHandler : IRequestHandler<UpdateProductStatusCommand, ResultDto>
{
    private readonly IStoreRespository _storeRespository;

    public UpdateProductStatusHandler(IStoreRespository storeRespository)
    {
        _storeRespository = storeRespository;
    }

    public Task<ResultDto> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
    {
        var update = _storeRespository.UpdateStatusProduct(request);

        return update;
    }
}