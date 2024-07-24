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

        if (response.Message.Isuccess == false)
        {
            return new ResponseResult
            {
                Isuccess = false,
                Message = $"message : {response.Message.Message}"
            };
        }

        return new ResponseResult
        {
            Isuccess = true,
            Message = $"message {response.Message.Message}"
        };
    }

//     public async Task<ResponseResult> CheckOutStore(Guid productId)
//     {
//         var response = await _updateStoreStatusPro.GetResponse<ResponseResult>(new CheckOutStore
//         {
//             ProductId = productId
//         });
//         if (response.Message.Isuccess == false)
//         {
//             return new ResponseResult
//             {
//                 Isuccess = false,
//                 Message = $"message : {response.Message.Message}"
//             };
//         }
//
//         return new ResponseResult
//         {
//             Isuccess = true,
//             Message = $"message {response.Message.Message}"
//         };
//     }
}