namespace Order.Domain.Model;

public class OrderModel : EntityBase
{
    public string UserId { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string? ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool OrderPaid { get; set; }
    public DateTime OrderPlaced { get; set; }
    public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    public int TotalPrice { get; set; }

    public OrderModel()
    {
        Id = Guid.NewGuid();
    }

    public OrderModel(Guid id)
    {
        Id = id;
    }
}