using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Constants;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using QuickHire.Infrastructure.Authentication.Processors;
using QuickHire.Infrastructure.Persistence.Identity;
using System.Security.Claims;

namespace QuickHire.Infrastructure.Authentication;

internal class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationUserRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthTokenProcessor _authTokenProcessor;
    private readonly IRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRole> roleManager, SignInManager<ApplicationUser> signInManager, IAuthTokenProcessor authTokenProcessor, IRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _authTokenProcessor = authTokenProcessor;
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AssignJwtToken(ApplicationUserModel applicationUserModel)
    {
        var user = await GetUserByIdAsync(applicationUserModel.Id);
        var roles = await _userManager.GetRolesAsync(user);
        var token = _authTokenProcessor.GenerateToken(user, roles);
        _authTokenProcessor.WriteTokeToCookie("ACCESS_TOKEN", token.jwtToken, token.expirationTime);
    }

    public async Task AssignRefreshToken(ApplicationUserModel applicationUserModel)
    {
        var user = await GetUserByIdAsync(applicationUserModel.Id);
        var refreshToken = _authTokenProcessor.GenerateRefreshToken();
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAt = refreshTokenExpiresAt;
        await _repository.UpdateAsync(user);

        _authTokenProcessor.WriteTokeToCookie("REFRESH_TOKEN", refreshToken, refreshTokenExpiresAt);
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUserModel applicationUserModel, string password)
    {
        var user = await GetUserByIdAsync(applicationUserModel.Id);
        var result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }

    public async Task<CreatedUserResultModel> CreateUserAsync(string email, string password)
    {
        var applicationUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = email,
            Email = email,
            EmailConfirmed = false,
            JoinedAt = DateTime.Now,
        };

        var result = await _userManager.CreateAsync(applicationUser, password);

        if (!result.Succeeded)
        {
            return new CreatedUserResultModel()
            {
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }
        var buyer = new Buyer
        {
            UserId = applicationUser.Id
        };
        await _repository.AddAsync(buyer);
        await _userManager.AddToRoleAsync(applicationUser, QuickHire.Domain.Shared.Constants.UserRoles.Buyer);

        return new CreatedUserResultModel()
        {
            IsSuccess = true
        };
    }

    public async Task<CreatedUserResultModel> CreateUserForExternalLoginAsync(ExternalLoginInfo externalLoginInfo)
    {
        var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

        var applicationUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            JoinedAt = DateTime.UtcNow,
        };

        var createResult = await _userManager.CreateAsync(applicationUser);
        if (!createResult.Succeeded)
        {
            return new CreatedUserResultModel()
            {
                IsSuccess = false,
                Errors = createResult.Errors.Select(x => x.Description).ToArray()
            };
        }

        // Link the external login (e.g., Google)
        var loginResult = await _userManager.AddLoginAsync(applicationUser, externalLoginInfo);
        if (!loginResult.Succeeded)
        {
            return new CreatedUserResultModel()
            {
                IsSuccess = false,
                Errors = loginResult.Errors.Select(x => x.Description).ToArray()
            };
        }

        // Create buyer profile
        var buyer = new Buyer
        {
            UserId = applicationUser.Id
        };
        await _repository.AddAsync(buyer);

        // Assign role
        await _userManager.AddToRoleAsync(applicationUser, UserRoles.Buyer);

        return new CreatedUserResultModel()
        {
            IsSuccess = true
        };
    }


    public async Task<ApplicationUserModel?> FindByExternalLoginAsync(string loginProvider, string providerKey)
    {
        var user = await _userManager.FindByLoginAsync(loginProvider, providerKey);
        return user != null ? new ApplicationUserModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed,
        } : null;
    }

    public async Task<string> GenerateEmailVerificationTokenAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<ApplicationUserModel> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (userId == null)
        {
            throw new NotFoundException("ApplicationUser", userId);
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("ApplicationUser", userId);
        }

        return new ApplicationUserModel()
        {
            Id = user.Id,
            UserName = user.UserName,
        };
    }

    public string GetCurrentUserIdAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (userId == null)
        {
            throw new NotFoundException("ApplicationUser", userId);
        }
        return userId;
    }

    public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
    {
        return await _signInManager.GetExternalLoginInfoAsync();
    }

    public async Task<RoleFilterItemModel[]> GetRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles.Select(x => new RoleFilterItemModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToArray();
    }

    public async Task<ApplicationUserModel> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null ? new ApplicationUserModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed,
        } : null;
    }

    public async Task<ApplicationUserModel> GetUserByRefreshTokenAsync(string token)
    {
        var user =  await _repository.GetAllReadOnly<ApplicationUser>()
            .FirstOrDefaultAsync(x => x.RefreshToken == token);

        return user != null ? new ApplicationUserModel()
        {
            Id = user.Id,
            RefreshTokenExpirationDate = user.RefreshTokenExpiresAt
        } : null;
    }

    public async Task<ApplicationUserModel> GetUserByUserIdAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        return user != null ? new ApplicationUserModel()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed,
        } : null;
    }

    public async Task<ApplicationUserModel> GetUserByUsernameOrEmailAsync(string emailOrUsername)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.UserName == emailOrUsername || x.Email == emailOrUsername) ;
        return user != null ? new ApplicationUserModel()
        {
            Id = user.Id,
            Email = user?.Email,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed,
        } : null;
    }

    public async Task<string> GetUserIdByBuyerIdAsync(int buyerId)
    {
        return await _repository.GetAllReadOnly<Buyer>().Where(x => x.Id == buyerId).Select(x => x.UserId).FirstOrDefaultAsync();
    }

    public async Task<string> GetUserIdBySellerIdAsync(int sellerId)
    {
        return await _repository.GetAllReadOnly<Seller>().Where(x => x.Id == sellerId).Select(x => x.UserId).FirstOrDefaultAsync();
    }

    public async Task LogoutUserAsync(ApplicationUserModel user)
    {
        var appUser = await GetUserByIdAsync(user.Id);

        appUser.RefreshToken = null;
        appUser.RefreshTokenExpiresAt = null;

        await _repository.UpdateAsync(appUser);

        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete("ACCESS_TOKEN");
        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete("REFRESH_TOKEN");
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<VerifyEmailResultModel> VerifyEmailAsync(string userId, string token)
    {
        var user = await GetUserByIdAsync(userId);
        var result = await _userManager.ConfirmEmailAsync(user, token);

        if(result.Succeeded)
        {
            user.EmailConfirmed = true;
            await _repository.UpdateAsync(user);
            return new VerifyEmailResultModel()
            {
                IsSuccess = true,
            };
        }
        else
        {
            return new VerifyEmailResultModel()
            {
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }
    }

    private async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("ApplicationUser", userId);
    }
}
