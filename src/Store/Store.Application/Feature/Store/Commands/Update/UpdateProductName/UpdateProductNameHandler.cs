using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Application.Feature.Store.Commands.Update.UpdateProductName;

public class UpdateProductNameHandler : IRequestHandler<UpdateProductNameCommand, ResultDto>
{
    private readonly IStoreRepository _storeRepository;

    public UpdateProductNameHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public Task<ResultDto> Handle(UpdateProductNameCommand request, CancellationToken cancellationToken)
    {
        var update = _storeRepository.UpdateProductName(request);
        return update;
    }
}