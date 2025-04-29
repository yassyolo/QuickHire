
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task<CreatedUserResultModel> CreateUserAsync(RegisterUserModel model);
    Task<string> GenerateEmailVerificationTokenAsync(string userId);
    Task<ApplicationUserModel> GetUserByEmailAsync(string email);
    Task<ApplicationUserModel> GetUserByUserIdAsync(string userId);
    Task<bool> UserExistsAsync(string email);
    Task<bool> VerifyEmailAsync(string userId, string token);
}
