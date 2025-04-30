
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task AssignJwtToken(ApplicationUserModel applicationUserModel);
    Task AssignRefreshToken(ApplicationUserModel applicationUserModel);
    Task<bool> CheckPasswordAsync(ApplicationUserModel applicationUserModel, string password);
    Task<CreatedUserResultModel> CreateUserAsync(RegisterUserModel model);
    Task<string> GenerateEmailVerificationTokenAsync(string userId);
    Task<ApplicationUserModel> GetUserByEmailAsync(string email);
    Task<ApplicationUserModel> GetUserByRefreshTokenAsync(string token);
    Task<ApplicationUserModel> GetUserByUserIdAsync(string userId);
    Task<ApplicationUserModel> GetUserByUsernameOrEmailAsync(string emailOrUsername);
    Task<bool> UserExistsAsync(string email);
    Task<VerifyEmailResultModel> VerifyEmailAsync(string userId, string token);
}
