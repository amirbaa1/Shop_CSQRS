using MediatR;
using Product.Domain.Model.Dto;

namespace Product.Application.Features.Product.Commands.Update;

public class UpdateProductCommand : UpdateProductDto ,IRequest<string>
{
    
}