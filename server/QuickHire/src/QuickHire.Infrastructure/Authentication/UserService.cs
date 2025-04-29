using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using QuickHire.Infrastructure.Persistence.Identity;

namespace QuickHire.Infrastructure.Authentication;

internal class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository _repository;

    public UserService(UserManager<ApplicationUser> userManager, IRepository repository)
    {
        _userManager = userManager;
        _repository = repository;
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

        return new CreatedUserResultModel()
        {
            IsSuccess = true,
        };
    }

    public async Task<string> GenerateEmailVerificationTokenAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
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

    public async Task<ApplicationUserModel> GetUserByUserIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
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

    public async Task VerifyEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found", $"User with id: {userId} not found.");
        var result = await _userManager.ConfirmEmailAsync(user, token);

        if(result.Succeeded)
        {
            user.EmailConfirmed = true;
            await _repository.UpdateAsync(user);
        }
        else
        {
            throw new BadRequestException("Email verification failed", result.Errors.Select(x => x.Description).ToArray());
        }
    }
}
