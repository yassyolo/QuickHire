using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Authentication.Register;

internal class RegisterBuyerCommandHandler : ICommandHandler<RegisterBuyerCommand, RegisterUserResponseModel>
{
    private readonly IUserService _userService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly INotificationService _notificationService;

    public RegisterBuyerCommandHandler(IUserService userService, IEmailSenderService emailSenderService, INotificationService notificationService)
    {
        _userService = userService;
        _emailSenderService = emailSenderService;
        _notificationService = notificationService;
    }

    public async Task<RegisterUserResponseModel> Handle(RegisterBuyerCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userService.UserExistsAsync(request.Email);

        if (userExists)
        {
            throw new ConflictException("User already exists", $"Email: {request.Email} already in use.");
        }

        try
        {
            var createdUserResult = await _userService.CreateUserAsync(request.Email, request.Password);

            if (!createdUserResult.IsSuccess)
            {
                throw new BadRequestException("User registration failed", string.Join("; ", createdUserResult.Errors.Select(e => e.ToString())));
            }

            var user = await _userService.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException("ApplicationUser", request.Email);
            }

            var emailVerificationToken = await _userService.GenerateEmailVerificationTokenAsync(user.Id);

            var verificationLink = $"https://localhost:7267/auth/verify-email?userId={user.Id}&token={Uri.EscapeDataString(emailVerificationToken)}";

            var emailModel = new EmailModel
            {
                To = user.Email!,
                Subject = "Email Verification",
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
                    .verify-button {{
                        display: inline-block;
                        margin-top: 20px;
                        padding: 10px 20px;
                        background-color: #1DBF73;
                        color: #ffff;
                        text-decoration: none;
                        border-radius: 5px;
                        font-weight: bold;
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
                        <h2>Welcome to QuickHire!</h2>
                        <p>Thanks for signing up. Please verify your email address by clicking the button below:</p>
                        <a href='{verificationLink}' class='verify-button'>Verify Email</a>
                    </div>
                    <div class='footer'>
                        &copy; {DateTime.Now.Year} QuickHire. All rights reserved.
                    </div>
                </div>
            </body>
        </html>"
            };

            await _emailSenderService.SendEmailAsync(emailModel, cancellationToken);
          
            return new RegisterUserResponseModel
            {
                Username = user.UserName!
            };
          
        }
        catch (Exception ex)
        {
            throw new BadRequestException("User registration failed", ex.Message);
        }        
    }
}
