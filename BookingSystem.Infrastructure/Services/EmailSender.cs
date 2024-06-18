using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookingSystem.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly string? apiKey;
    public EmailSender(IConfiguration config)
    {
        apiKey = config.GetSection("SendGridEmailSender").GetValue<string>("API_KEY");
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(email, subject, htmlMessage);
    }

    public async Task Execute(string email, string subject, string htmlMessage)
    {
    var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage
        {
            From = new EmailAddress("isac.a.rosenberg@gmail.com", "Password recovery"),
            Subject = subject,
            PlainTextContent = htmlMessage,
            HtmlContent = htmlMessage
        };
        msg.AddTo(new EmailAddress(email));

        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        
    }
}
