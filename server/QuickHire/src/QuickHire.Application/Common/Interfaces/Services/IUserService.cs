
using Microsoft.AspNetCore.Identity;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task AssignJwtToken(ApplicationUserModel applicationUserModel);
    Task AssignRefreshToken(ApplicationUserModel applicationUserModel);
    Task<bool> CheckPasswordAsync(ApplicationUserModel applicationUserModel, string password);
    Task<CreatedUserResultModel> CreateUserAsync(string email, string password);
    Task<string> GenerateEmailVerificationTokenAsync(string userId);
    Task<ApplicationUserModel> GetCurrentUserAsync();
    Task<string> GetUserIdByBuyerIdAsync(int buyerId);
    Task<ApplicationUserModel> GetUserByEmailAsync(string email);
    Task<ApplicationUserModel> GetUserByRefreshTokenAsync(string token);
    Task<ApplicationUserModel> GetUserByUserIdAsync(string userId);
    Task<ApplicationUserModel> GetUserByUsernameOrEmailAsync(string emailOrUsername);
    Task LogoutUserAsync(ApplicationUserModel user);
    Task<bool> UserExistsAsync(string email);
    Task<VerifyEmailResultModel> VerifyEmailAsync(string userId, string token);
    Task<string> GetUserIdBySellerIdAsync(int sellerId);
    Task<RoleFilterItemModel[]> GetRolesAsync();
    string GetCurrentUserIdAsync();
    Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
    Task<ApplicationUserModel?> FindByExternalLoginAsync(string loginProvider, string providerKey);
    Task<CreatedUserResultModel> CreateUserForExternalLoginAsync(ExternalLoginInfo externalLoginInfo);
}
