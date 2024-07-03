using Order.Domain.Model.Email;

namespace Order.Domain.Repository;

public interface IEmailSend
{
    Task<bool> Send(EmailModel email);
}