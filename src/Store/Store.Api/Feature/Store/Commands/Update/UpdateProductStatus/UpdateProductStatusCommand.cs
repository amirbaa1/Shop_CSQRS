using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Api.Feature.Store.Commands.Update.UpdateProductStatus;

public class UpdateProductStatusCommand : UpdateStatusProductDto, IRequest<ResultDto>
{
}