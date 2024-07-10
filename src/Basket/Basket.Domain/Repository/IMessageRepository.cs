using Basket.Domain.Model.Dto;

namespace Basket.Domain.Repository;

public interface IMessageRepository
{
    Task<bool> MessageResultSet(ResultDto resultDto);
    Task<ResultDto> GetMessageResult(string productId);
}