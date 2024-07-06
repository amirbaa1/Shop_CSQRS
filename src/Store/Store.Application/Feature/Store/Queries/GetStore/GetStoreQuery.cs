

using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Application.Feature.Store.Queries.GetStore
{
    public class GetStoreQuery : IRequest<List<StoreDto>>
    {
    }
}
