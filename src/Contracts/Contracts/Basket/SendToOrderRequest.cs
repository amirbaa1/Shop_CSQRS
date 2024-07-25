using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Basket
{
    public class SendToOrderRequest
    {
        public Guid BasketId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? ZipCode { get; set; }
        public string EmailAddress { get; set; }

        public int TotalPrice { get; set; }
        public List<BasketItemRequest> BasketItems { get; set; } = new List<BasketItemRequest>();

    }

    public class BasketItemRequest
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
