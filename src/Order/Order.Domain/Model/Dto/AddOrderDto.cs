namespace Order.Domain.Model.Dto;

public class AddOrderDto
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
    public List<AddOrderLineDto> OrderLines { get; set; } = new List<AddOrderLineDto>();
    public int TotalPrice { get; set; }
}