using Store.Domain.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repository
{
    public interface  IStoreRespository
    {
        Task<ResultDto> CreateStore(StoreDto storeDto);
        Task<ResultDto> UpdateStore(UpdateNumberDto update);
        Task<ResultDto> DeleteStore(Guid productId);
        Task<List<StoreDto>> GetStore();
    }
}
