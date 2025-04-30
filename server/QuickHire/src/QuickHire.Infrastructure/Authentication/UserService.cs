using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using QuickHire.Infrastructure.Authentication.Processors;
using QuickHire.Infrastructure.Persistence.Identity;

namespace QuickHire.Infrastructure.Authentication;

internal class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthTokenProcessor _authTokenProcessor;
    private readonly IRepository _repository;

    public UserService(UserManager<ApplicationUser> userManager, IRepository repository, IAuthTokenProcessor authTokenProcessor)
    {
        _userManager = userManager;
        _repository = repository;
        _authTokenProcessor = authTokenProcessor;
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

    public async Task<CreatedUserResultModel> CreateUserAsync(RegisterUserModel model)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = false,
            JoinedAt = DateTime.Now,
        };

        var buyer = new Buyer
        {
            UserId = applicationUser.Id
        };

        var result = await _userManager.CreateAsync(applicationUser, model.Password);

        if(!result.Succeeded)
        {
            return new CreatedUserResultModel()
            {
                IsSuccess = false,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }

        await _repository.AddAsync(buyer);
        await _userManager.AddToRoleAsync(applicationUser, "Buyer");

        return new CreatedUserResultModel()
        {
            IsSuccess = true,
        };
    }

    public async Task<string> GenerateEmailVerificationTokenAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
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
            Email = user.Email,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed,
        } : null;
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
        return await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found", $"User with id: {userId} not found.");
    }
}
