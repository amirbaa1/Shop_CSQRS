

using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Api.Feature.Store.Queries.GetStore
{
    public class GetStoreQuery : IRequest<List<StoreDto>>
    {
    }
}
