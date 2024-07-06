using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Application.Feature.Store.Commands.Update.UpdateProductName;

public class UpdateProductNameHandler : IRequestHandler<UpdateProductNameCommand, ResultDto>
{
    private readonly IStoreRespository _storeRespository;

    public UpdateProductNameHandler(IStoreRespository storeRespository)
    {
        _storeRespository = storeRespository;
    }

    public Task<ResultDto> Handle(UpdateProductNameCommand request, CancellationToken cancellationToken)
    {
        var update = _storeRespository.UpdateProductName(request);
        return update;
    }
}