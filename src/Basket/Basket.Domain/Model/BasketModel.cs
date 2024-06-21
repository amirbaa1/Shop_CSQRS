
namespace Basket.Domain.Model
{
    public class BasketModel
    {

        public Guid Id { get; set; }
        public string UserId { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        //public Guid? DiscountId { get; set; }

        public BasketModel(string userId)
        {
            UserId = userId;
        }

        public BasketModel()
        {
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Product.UnitPrice * item.Quantity;
                }
                return totalprice;
            }
        }

    }
}
