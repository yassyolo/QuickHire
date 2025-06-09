using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Moderation;

namespace QuickHire.Application.Admin.Users.DeactivateUser;

public class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;
    private readonly IEmailSenderService _emailSenderService;

    public DeactivateUserCommandHandler(IUserService userService, IRepository repository, IEmailSenderService emailSenderService)
    {
        _userService = userService;
        _repository = repository;
        _emailSenderService = emailSenderService;
    }

    public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByExistingsUserIdAsync(request.UserId);
        var gigsQueryable = _repository.GetAllReadOnly<Gig>().Where(x => x.SellerId == sellerId);
        var gigs = await _repository.ToListAsync<Gig>(gigsQueryable);

        foreach (var gig in gigs)
        {
            gig.ModerationStatus = Domain.Moderation.Enums.ModerationStatus.Deactivated;
        }

        var existingReportQueryable = _repository.GetAllReadOnly<ReportedItem>().Where(x => x.ReportedUserId == request.UserId);
        var existingReport = await _repository.FirstOrDefaultAsync<ReportedItem>(existingReportQueryable);

        var deactivationRecord = new DeactivatedRecord()
        {
            UserId = request.UserId,
            Reason = request.Reason,
            CreatedAt = DateTime.UtcNow,
        };

        await _repository.AddAsync(deactivationRecord);
        await _repository.SaveChangesAsync();

        await _userService.DeactivateUserAsync(request.UserId);
        var userEmail = await _userService.GetUserEmailByUserIdAsync(request.UserId);

        var emailModel = new EmailModel
        {
            To = userEmail,
            Subject = "Account Deactivation Notice",
            Body = $@"
<html>
    <head>
        <style>
            .email-container {{
                font-family: Arial, sans-serif;
                padding: 20px;
                background-color: #f9f9f9;
                color: #333;
                line-height: 1.6;
            }}
            .email-content {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            }}
            .reason-box {{
                margin-top: 15px;
                padding: 10px;
                background-color: #ffe6e6;
                border-left: 4px solid #e74c3c;
                color: #c0392b;
                border-radius: 4px;
            }}
            .footer {{
                margin-top: 30px;
                font-size: 12px;
                color: #999;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='email-content'>
                <h2>Account Deactivation</h2>
                <p>We're writing to inform you that your account on <strong>QuickHire</strong> has been deactivated.</p>
                <p>Please review the reason for this action below:</p>
                <div class='reason-box'>
                    <strong>Reason:</strong> {request.Reason}
                </div>
                <p>If you believe this was a mistake or would like to appeal, please contact our support team.</p>
            </div>
            <div class='footer'>
                &copy; {DateTime.Now.Year} QuickHire. All rights reserved.
            </div>
        </div>
    </body>
</html>"
        };

        await _emailSenderService.SendEmailAsync(emailModel, cancellationToken);        

        return Unit.Value;  
    }
}

