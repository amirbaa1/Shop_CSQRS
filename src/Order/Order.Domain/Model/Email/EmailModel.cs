namespace Order.Domain.Model.Email;

public class EmailModel
{
    public string To { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}