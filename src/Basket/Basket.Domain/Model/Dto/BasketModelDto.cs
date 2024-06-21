

namespace Basket.Domain.Model.Dto
{
    public class BasketModelDto
    {

        public Guid Id { get; set; }
        public string UserId { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        // public int TotalPrice { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.UnitPrice * item.Quantity;
                }
                return totalprice;
            }
        }

        public BasketModelDto(string userId)
        {
            UserId = userId;
        }

        public BasketModelDto()
        {
        }
    }
}
