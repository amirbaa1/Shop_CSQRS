using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Application.Feature.Store.Commands.Update.UpdateProductName;

public class UpdateProductNameCommand : UpdateProductNameDto, IRequest<ResultDto>
{
}