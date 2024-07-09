using System.Net;

namespace EventBus.Messages.Event.Store;

public class MessageCheckStoreEvent : IntegrationBaseEvent
{
    public Guid ProductId { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
}