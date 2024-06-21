using Basket.Domain.Model;
using Basket.Domain.Model.Dto;


namespace Basket.Domain.Repository
{
    public interface IBasketRepository
    {
        Task<BasketModelDto> GetOrCreateBasketForUser(string userId);
        Task<bool> AddBasket(AddItemToBasketDto basketitem);
        Task<string> RemoveItemFromBasket(Guid basketId);
        void UpdateQuantities(Guid basketitemId, int quantity);
        //ResultDto CheckOutBasket(CheckOutBasketDto checkOut, IDiscountService discountService);
    }
}
