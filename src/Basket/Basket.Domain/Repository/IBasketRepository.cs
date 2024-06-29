using Basket.Domain.Model;
using Basket.Domain.Model.Dto;


namespace Basket.Domain.Repository
{
    public interface IBasketRepository
    {
        Task<BasketModelDto> GetOrCreateBasketForUser(string userId);
        Task<bool> AddBasket(AddItemToBasketDto basketitem);
        Task<string> RemoveItemFromBasket(Guid basketId);
        Task<string> UpdateQuantities(Guid basketitemId, int quantity);
        Task<ResultDto> CheckOutBasket(CheckOutDto checkOut);
    }
}