namespace EventBus.Messages.Event.Product;

public class ProductStoreUpdateEvent
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
}