using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Application.Feature.Store.Queries.Check;

public class CheckStoreQuery : CheckNumberDto, IRequest<ResultDto>
{
}