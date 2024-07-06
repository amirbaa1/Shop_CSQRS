

using MediatR;
using Store.Domain.Model.Dto;

namespace Store.Application.Feature.Store.Commands.Delete
{
    public class DeleteStoreCommand : IRequest<ResultDto>
    {
        public Guid ProductId { get; set; }

        public DeleteStoreCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}
