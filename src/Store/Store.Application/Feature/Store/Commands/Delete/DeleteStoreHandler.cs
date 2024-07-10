using MediatR;
using Store.Domain.Model.Dto;
using Store.Domain.Repository;

namespace Store.Application.Feature.Store.Commands.Delete
{
    public class DeleteStoreHandler : IRequestHandler<DeleteStoreCommand, ResultDto>
    {
        private readonly IStoreRepository _store;

        public DeleteStoreHandler(IStoreRepository store)
        {
            _store = store;
        }

        public async Task<ResultDto> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var delete = await _store.DeleteStore(request.ProductId);
            return delete;
        }
    }
}
