
using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Api.Feature.Store.Commands.Create
{
    public class CreateStoreCommand : StoreDto, IRequest<ResultDto>
    {

    }
}
