using Common.Infrastructure.Service;
using Contracts.General;
using Contracts.Product;
using MassTransit;
using Store.Domain.Repository;

namespace Store.Api.Consumer;

public class ProductDeleteStore : IConsumer<ProductDeleteRequest>
{
    private readonly IStoreRepository _storeRepository;

    public ProductDeleteStore(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task Consume(ConsumeContext<ProductDeleteRequest> context)
    {
        var message = context.Message;

        var response = await _storeRepository.DeleteStore(message.productId);
        var result = new ResponseResult();

        result.Message = response.Message;
        result.Isuccess = response.IsSuccessful;

        await context.RespondAsync(result);
    }
}