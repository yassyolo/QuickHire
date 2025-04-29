using Azure.Core;
using Microsoft.Extensions.Options;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Infrastructure.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuickHire.Infrastructure.Communication;

internal class EmailSenderService : IEmailSenderService
{
    private readonly SendGridOptions _sendGridOptions;

    public EmailSenderService(IOptions<SendGridOptions> sendGridOptions)
    {
        _sendGridOptions = sendGridOptions.Value;
    }

    public async Task SendEmailAsync(EmailModel emailModel, CancellationToken cancellationToken = default)
    {
        var key = _sendGridOptions.ApiKey;
        var client = new SendGridClient(key);
        var from = new EmailAddress(_sendGridOptions.FromEmail, _sendGridOptions.FromName);
        var subject = emailModel.Subject;
        var to = new EmailAddress(emailModel.To, emailModel.To);
        var msg = MailHelper.CreateSingleEmail(from, to, emailModel.Subject, plainTextContent: subject, htmlContent: subject);

        var response = await client.SendEmailAsync(msg, cancellationToken);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new InternalServerErrorException("Failed to send email.", $"Failed to send email to {to}.");
        }
    }
}
