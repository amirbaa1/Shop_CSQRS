using MediatR;
using Product.Domain.Model;



namespace Product.Application.Features.Product.Commands.Update.UpdateProductStatus
{
    public class ProductStatusCommand : IRequest<string>
    {
        public Guid ProductId { get; set; }
        public int Number { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
