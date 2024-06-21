

namespace Basket.Domain.Model.Dto
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public string ImageUrl { get; set; }

        //public List<BasketItem> basketItems { get; set; }
    }
}
