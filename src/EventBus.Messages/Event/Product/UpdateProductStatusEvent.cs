

namespace EventBus.Messages.Event.Product
{
    public class UpdateProductStatusEvent
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public ProductStatusEvent ProductStatus { get; set; }
        public int Number { get; set; }
    }
}
