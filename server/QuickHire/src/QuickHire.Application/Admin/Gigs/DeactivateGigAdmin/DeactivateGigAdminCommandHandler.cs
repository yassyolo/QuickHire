using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Moderation;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Domain.Shared.Exceptions;


namespace QuickHire.Application.Admin.Gigs.DeactivateGigAdmin;

public class DeactivateGigAdminCommandHandler : ICommandHandler<DeactivateGigAdminCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly IEmailSenderService _emailService;

    public DeactivateGigAdminCommandHandler(IRepository repository, IUserService userService, IEmailSenderService emailService)
    {
        _repository = repository;
        _userService = userService;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(DeactivateGigAdminCommand request, CancellationToken cancellationToken)
    {
        var gisQueryable = _repository.GetAllIncluding<Gig>(x => x.Orders).Where(x => x.Id == request.Id);
        var gig = await _repository.FirstOrDefaultAsync(gisQueryable);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        if(gig.ModerationStatus == ModerationStatus.Deactivated)
        {
            throw new InvalidOperationException("This gig is already deactivated.");
        }

        if(gig.Orders.Where(x => x.Status == Domain.Orders.Enums.OrderStatus.Paid || x.Status == Domain.Orders.Enums.OrderStatus.InProgress).Any())
        {
            throw new InvalidOperationException("This gig cannot be deactivated because it has active orders.");
        }
        try
        {
            gig.ModerationStatus = ModerationStatus.Deactivated;

            var deactivatedRecord = new DeactivatedRecord
            {
                GigId = request.Id,
                Reason = request.Reason,
                CreatedAt = DateTime.Now,
            };

            gig.ModerationStatus = ModerationStatus.Deactivated;

            await _repository.AddAsync(deactivatedRecord);
            await _repository.UpdateAsync(gig);

            await _repository.SaveChangesAsync();

            var gigSellerEmail = await _userService.GetGigSellerEmailAsync(request.Id);

            var emailModel = new EmailModel
            {
                To = gigSellerEmail!,
                Subject = "Gig Deactivation Notice",
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
            .gig-title {{
                font-size: 16px;
                font-weight: bold;
                margin-top: 10px;
                color: #2c3e50;
            }}
            .reason-box {{
                margin-top: 15px;
                padding: 10px;
                background-color: #fff3cd;
                border-left: 4px solid #f0ad4e;
                color: #856404;
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
                <h2>Your Gig Has Been Deactivated</h2>
                <p>We wanted to let you know that one of your gigs on <strong>QuickHire</strong> has been deactivated by our moderation team.</p>
                <div class='gig-title'>Gig Title: <span style='color:#1DBF73;'>{gig.Title}</span></div>
                <p>Reason for deactivation:</p>
                <div class='reason-box'>
                    {request.Reason}
                </div>
                <p>If you believe this was an error or need clarification, please reach out to our support team.</p>
            </div>
            <div class='footer'>
                &copy; {DateTime.Now.Year} QuickHire. All rights reserved.
            </div>
        </div>
    </body>
</html>"
            };

            await _emailService.SendEmailAsync(emailModel, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Failed to deactivate gig.", ex.Message);
        }
      
        return Unit.Value;
    }
}

