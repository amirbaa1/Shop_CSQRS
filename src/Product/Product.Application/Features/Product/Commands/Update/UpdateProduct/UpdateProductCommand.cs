using MediatR;
using Product.Domain.Model.Dto;

namespace Product.Application.Features.Product.Commands.Update.UpdateProduct;

public class UpdateProductCommand : UpdateProductDto, IRequest<string>
{

}