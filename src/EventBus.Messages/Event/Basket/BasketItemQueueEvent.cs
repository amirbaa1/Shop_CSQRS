﻿

namespace EventBus.Messages.Event.Basket
{
    public class BasketItemQueueEvent
    {
        public Guid BasketItemId { get; set; }
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
