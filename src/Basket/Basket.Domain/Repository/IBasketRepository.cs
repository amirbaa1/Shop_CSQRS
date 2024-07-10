using Basket.Domain.Model;
using Basket.Domain.Model.Dto;


namespace Basket.Domain.Repository
{
    public interface IBasketRepository
    {
        Task<BasketModelDto> GetOrCreateBasketForUser(string userId);
        Task<string> AddBasket(AddItemToBasketDto basketItem);
        Task<string> RemoveItemFromBasket(Guid basketId);
        Task<string> UpdateQuantities(Guid basketItemId, int quantity);
        Task<ResultDto> CheckOutBasket(CheckOutDto checkOut);
    }
}