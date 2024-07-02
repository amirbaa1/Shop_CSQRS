using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Order.Domain.Model.Email;
using Order.Domain.Repository;

namespace Order.Infrastructure.Repository;


public class EmailSend : IEmailSend
{
    public EmailSend(ILogger<EmailSend> logger)
    {
        _logger = logger;
    }

    private EmailSetting _emailSetting { get; }
    private readonly ILogger<EmailSend> _logger;

    public async Task<bool> Send(EmailModel email)
    {
        try
        {
            var message = new MailMessage(email.From, email.To, email.Subject, email.Body);


            using (var emailClient = new SmtpClient(_emailSetting.HOST, _emailSetting.PORT))
            {
                emailClient.Credentials = new NetworkCredential(_emailSetting.User, _emailSetting.Password);
                await emailClient.SendMailAsync(message);
                _logger.LogInformation($"--->sand Email : {email.To}");
                _logger.LogInformation($"--->sand Email : {emailClient.SendMailAsync(message)}");
            }

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}