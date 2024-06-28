namespace Basket.Domain.Model.Dto;

public class CheckOutDto
{
    public Guid BasketId { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string? ZipCode { get; set; }
    public string EmailAddress { get; set; }
}