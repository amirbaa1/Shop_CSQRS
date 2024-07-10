namespace EventBus.Messages.Event.Basket;

public class BasketStoreEvent
{
    public Guid ProductId { get; set; }
    public int Number { get; set; }
}