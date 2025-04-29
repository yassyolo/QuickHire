using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface IEmailSenderService
{
    Task SendEmailAsync(EmailModel emailModel, CancellationToken cancellationToken = default);
}
