using Microsoft.Extensions.Options;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Infrastructure.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace QuickHire.Infrastructure.Communication;

internal class EmailSenderService : IEmailSenderService
{
    private readonly SendGridOptions _sendGridOptions;

    public EmailSenderService(IOptions<SendGridOptions> options)
    {
        _sendGridOptions = options.Value;
    }

    public async Task SendEmailAsync(EmailModel emailModel, CancellationToken cancellationToken = default)
    {
        var client = new SendGridClient(_sendGridOptions.ApiKey);
        var from = new EmailAddress(_sendGridOptions.FromEmail, _sendGridOptions.FromName);
        var to = new EmailAddress(emailModel.To, emailModel.To);
        var msg = MailHelper.CreateSingleEmail(from, to, emailModel.Subject, plainTextContent: "Please verify your email address by clicking the link.", htmlContent: emailModel.Body);

        var response = await client.SendEmailAsync(msg, cancellationToken);
        if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
        {
            throw new Exception($"Failed to send email. Status code: {response.StatusCode}, Response: {await response.Body.ReadAsStringAsync(cancellationToken)}");
        }
    }
}
