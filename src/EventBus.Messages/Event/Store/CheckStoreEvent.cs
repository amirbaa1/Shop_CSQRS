namespace EventBus.Messages.Event.Store;

public class CheckStoreEvent : IntegrationBaseEvent
{
    public Guid ProductId { get; set; }
    public int Number { get; set; }
}