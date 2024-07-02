namespace EventBus.Messages.Event.Product;

public class ProductQueueEvent : IntegrationBaseEvent
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}