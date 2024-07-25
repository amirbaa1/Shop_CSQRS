using Contracts.General;
using Contracts.Product;
using Contracts.Store;
using MassTransit;

namespace Common.Infrastructure.Service;

public class StoreService
{
    private readonly IRequestClient<UpdateStoreStatusRequest> _updateStoreStatusPro;

    public StoreService(IRequestClient<UpdateStoreStatusRequest> updateStoreStatusPro)
    {
        _updateStoreStatusPro = updateStoreStatusPro;
    }

    public async Task<ResponseResult> UpdateStoreStatus(Guid productId, ProductStatusRequest productStatusRequest,
        int price, int number)
    {
        var response = await _updateStoreStatusPro.GetResponse<ResponseResult>(new UpdateStoreStatusRequest
        {
            ProductId = productId,
            ProductStatus = productStatusRequest,
            Price = price,
            Number = number
        });

        if (response.Message.IsSuccessful == false)
        {
            return new ResponseResult
            {
                IsSuccessful = false,
                Message = $"message : {response.Message.Message}"
            };
        }

        return new ResponseResult
        {
            IsSuccessful = true,
            Message = $"message {response.Message.Message}"
        };
    }
}