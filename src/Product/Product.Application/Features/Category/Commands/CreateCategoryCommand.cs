using MediatR;
using Product.Domain.Model.Dto;

namespace Product.Application.Features.Category.Commands;

public class CreateCategoryCommand : CategoryDto, IRequest<string>
{
}