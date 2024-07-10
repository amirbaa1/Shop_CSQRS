using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Domain.Model.Email;
using Order.Domain.Repository;

namespace Order.Infrastructure.Repository;


public class EmailSend : IEmailSend
{
    private readonly EmailConfig _config;
    private readonly ILogger<EmailSend> _logger;

    public EmailSend(IOptions<EmailConfig> config, ILogger<EmailSend> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public async Task<bool> Send(EmailModel email)
    {
        try
        {
            var message = new MailMessage(email.From, email.To, email.Subject, email.Body);


            using (var emailClient = new SmtpClient(_config.HOST, _config.PORT))
            {
                emailClient.Credentials = new NetworkCredential(_config.User, _config.Password);
                await emailClient.SendMailAsync(message);
                _logger.LogInformation($"--->sand Email : {email.To}");
            }

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}