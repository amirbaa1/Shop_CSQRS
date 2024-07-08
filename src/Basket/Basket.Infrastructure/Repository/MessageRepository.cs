using Basket.Domain.Model.Dto;
using Basket.Domain.Repository;

namespace Basket.Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    
    public ResultDto MessageResult(ResultDto resultDto)
    {
        return new ResultDto
        {
            StatusCode = resultDto.StatusCode,
            IsSuccess = resultDto.IsSuccess,
            Message = resultDto.Message
        };
    }
    
}