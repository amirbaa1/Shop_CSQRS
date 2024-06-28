

namespace EventBus.Messages.Event
{
    public class BasketQueueEvent : IntegrationBaseEvent
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

        public List<BasketItemQueueEvent> BasketItems { get; set; } = new List<BasketItemQueueEvent>();
    }
}
