using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Api.Feature.Store.Queries.Check;

public class CheckStoreQuery : CheckNumberDto, IRequest<ResultDto>
{
}