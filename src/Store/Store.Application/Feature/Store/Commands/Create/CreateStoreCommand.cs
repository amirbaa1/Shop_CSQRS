
using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Application.Feature.Store.Commands.Create
{
    public class CreateStoreCommand : StoreDto, IRequest<ResultDto>
    {

    }
}
