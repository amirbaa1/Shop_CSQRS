using Basket.Domain.Model.Dto;

namespace Basket.Domain.Repository;

public interface IMessageRepository
{
    ResultDto MessageResult(ResultDto resultDto);
}