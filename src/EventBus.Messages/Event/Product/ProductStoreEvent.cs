namespace EventBus.Messages.Event.Product;

public class ProductStoreEvent
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public ProductStatusEvent ProductStatusEvent { get; set; }
    public int Number { get; set; }
}
