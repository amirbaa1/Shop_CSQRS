using Contracts.General;
using Contracts.Product;
using MassTransit;
using Store.Domain.Repository;

namespace Store.Worker.Consumer;

public class ProductDeleteStoreConsumer : IConsumer<ProductDeleteRequest>
{
    private readonly IStoreRepository _storeRepository;

    public ProductDeleteStoreConsumer(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task Consume(ConsumeContext<ProductDeleteRequest> context)
    {
        var message = context.Message;

        var response = await _storeRepository.DeleteStore(message.productId);
        var result = new ResponseResult();

        result.Message = response.Message;
        result.IsSuccessful = response.IsSuccessful;
        result.StatusCode = response.StatusCode;

        await context.RespondAsync(result);
    }
}