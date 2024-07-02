using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Order.Domain.Model.Email;
using Order.Domain.Repository;

namespace Order.Infrastructure.Repository;
//
// public class EmailSetting
// {
//     //stmp email
//     public string HOST { get; set; } = "smtp.elasticemail.com";
//     public int PORT { get; set; } = 2525;
//     public string User { get; set; } = "amir.2002.ba@gmail.com";
//     public string Password { get; set; } = "69985A8E31CB9CF00B0284B8A37C1EDEE8D8";

    // sandGrid
    // public string ApiKey { get; set; } = "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";
    // public string FromAddress { get; set; } = "amir.2002.ba@gmail.com";
    // public string FromName { get; set; } = "shooping";
// }

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