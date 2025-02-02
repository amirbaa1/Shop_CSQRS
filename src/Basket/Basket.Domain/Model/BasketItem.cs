﻿

namespace Basket.Domain.Model
{
    public class BasketItem 
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public BasketModel basket { get; set; }
        public Guid BasketId { get; set; }
        public void SetQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}

