using Store.Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repository
{
    public interface IStoreRepository
    {
        Task<ResultDto> CreateStore(StoreDto storeDto);
        Task<ResultDto> UpdateInventoryAfterPurchase(UpdateNumberDto update);
        Task<ResultDto> UpdateProductName(UpdateProductNameDto updateName);
        Task<ResultDto> UpdateStatusProduct(UpdateStatusProductDto updateStatusProductDto);
        Task<ResultDto> DeleteStore(Guid productId);
        Task<List<StoreDto>> GetStore();
        Task<StoreDto> GetStoreByProductId(Guid productId);
        Task<ResultDto> CheckStore(CheckNumberDto checkNumberDto);
    }
}