using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Admin.Models.Users;
using QuickHire.Application.Admin.Users.SearchUsers;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Application.Users.Models.NewSEller;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Constants;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using QuickHire.Infrastructure.Authentication.Processors;
using QuickHire.Infrastructure.Persistence.Identity;
using System.Security.Claims;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Infrastructure.Authentication;

internal class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthTokenProcessor _authTokenProcessor;
    private readonly IRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IAuthTokenProcessor authTokenProcessor, IRepository repository, ICloudinaryService cloudinaryService, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _authTokenProcessor = authTokenProcessor;
        _repository = repository;
        _cloudinaryService = cloudinaryService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AssignJwtTokens(ApplicationUserModel applicationUserModel, string mode)
    
    {
        var user = await _repository.GetAll<ApplicationUser>().Where(x => x.Id == applicationUserModel.Id).FirstOrDefaultAsync() ?? throw new NotFoundException("ApplicationUser", applicationUserModel.Id);
        var roles = await _userManager.GetRolesAsync(user);
        var token = _authTokenProcessor.GenerateToken(user, roles, mode);
        _authTokenProcessor.WriteTokeToCookie("ACCESS_TOKEN", token.jwtToken, token.expirationTime);

        var refreshToken = _authTokenProcessor.GenerateRefreshToken();
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAt = refreshTokenExpiresAt;
        await _repository.UpdateAsync(user);

        _authTokenProcessor.WriteTokeToCookie("REFRESH_TOKEN", refreshToken, refreshTokenExpiresAt);
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

    public async Task ChangePasswordAsync(string newPassword)
    {
        var user = await GetCurrentApplicationUserAsync();
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, newPassword);
    }

    private async Task<ApplicationUser> GetCurrentApplicationUserAsync()
    {
        var userId = GetCurrentUserIdAsync();
        return await _userManager.FindByIdAsync(userId);
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
            ModerationStatus = QuickHire.Domain.Moderation.Enums.ModerationStatus.Active,
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
        await _repository.SaveChangesAsync();

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

        var loginResult = await _userManager.AddLoginAsync(applicationUser, externalLoginInfo);
        if (!loginResult.Succeeded)
        {
            return new CreatedUserResultModel()
            {
                IsSuccess = false,
                Errors = loginResult.Errors.Select(x => x.Description).ToArray()
            };
        }

        var buyer = new Buyer
        {
            UserId = applicationUser.Id
        };
        await _repository.AddAsync(buyer);

        await _userManager.AddToRoleAsync(applicationUser, UserRoles.Buyer);

        return new CreatedUserResultModel()
        {
            IsSuccess = true
        };
    }

    public async Task DeactivateUserAsync(string userId)
    {
        var user = await GetUserByIdAsync(userId);
        user.ModerationStatus = QuickHire.Domain.Moderation.Enums.ModerationStatus.Deactivated;

        await _repository.UpdateAsync(user);
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

    public async Task<int> GetBuyerIdByUserIdAsync()
    {
        var userId = GetCurrentUserIdAsync();
        var buyer = await _repository.GetAllReadOnly<Buyer>().FirstOrDefaultAsync(x => x.UserId == userId);
        if (buyer == null)
        {
            throw new NotFoundException(nameof(Buyer), userId);
        }
        return buyer.Id;
    }

    public async Task<(string Name, string ProfilePictureUrl, string MemberSince, string Location, string[] Languages)> GetUserInfoForPreviewAsync(string userId)
    {      
        var address = await _repository.GetAllReadOnly<Address>().Where(x => x.UserId == userId).Include(x => x.Country).Select(x => x.Country.Name).FirstOrDefaultAsync();

        var user = await _userManager.FindByIdAsync(userId);
        return (user.FullName, user.ProfileImageUrl ?? string.Empty, user.JoinedAt.ToString("MMMM dd, yyyy"),  address ?? string.Empty, await _repository.GetAllReadOnly<UserLanguage>().Where(x => x.UserId == userId).Select(x => x.Language.Name).ToArrayAsync());
    }

    public async Task<BuyerProfileModel> GetBuyerProfileAsync()
    {       
        var userId = GetCurrentUserIdAsync();
        var user = await _userManager.FindByIdAsync(userId);
        var countryName = await _repository.GetAllReadOnly<Address>().Where(x => x.UserId == userId).Include(x => x.Country).Select(x => x.Country.Name).FirstOrDefaultAsync();

        var userLanguages   = await _repository.GetAllReadOnly<UserLanguage>().Where(x => x.UserId == userId).Include(x => x.Language).ToListAsync();

        return new BuyerProfileModel()
        {
            ProfilePictureUrl = user.ProfileImageUrl ?? string.Empty,
            FullName = user.FullName,
            Location = countryName ?? string.Empty,
            MemberSince = user.JoinedAt.ToString("MMMM dd, yyyy"),
            Username = user.UserName,
            Description = user.Description ?? string.Empty,
            Languages = userLanguages.Select(x => new UserLanguageModel()
            {
                LanguageName = x.Language.Name,
                LanguageId = x.LanguageId,
            }).ToArray(),
        };
    }

    public async Task<(string fullName, bool repeatBuyer, string countryName, string profileImageUrl)> GetBuyerReviewDetailsAsync(int buyerId, int sellerId)
    {
        var userId = await GetUserIdByBuyerIdAsync(buyerId);
        var user = await _userManager.FindByIdAsync(userId);
        var country = await _repository.GetAllReadOnly<Address>().Where(x => x.UserId == userId).Include(x => x.Country).Select(x => x.Country.Name).FirstOrDefaultAsync();

        var repeatBuyer = await _repository.GetAllReadOnly<Order>().Where(x => x.BuyerId == buyerId && x.SellerId == sellerId).CountAsync() > 2;
        
        return (user.FullName, repeatBuyer, country ?? string.Empty, user.ProfileImageUrl ?? string.Empty);
    }

    public async Task<ApplicationUserModel> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
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
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        return userId;
    }

    public async Task<GetExistingUserInfoModel> GetExistingUserInfoAsync()
    {
        var userId = GetCurrentUserIdAsync();
        var user = await _userManager.FindByIdAsync(userId);
        return new GetExistingUserInfoModel()
        {
            FullName = user.FullName,
            Username = user.UserName,
            Description = user.Description ?? string.Empty,
            ProfilePictureUrl = user.ProfileImageUrl ?? string.Empty,
            Languages = await _repository.GetAllReadOnly<UserLanguage>().Where(x => x.UserId == userId).Include(x => x.Language)
                .Select(x => new UserLanguageModel
                {
                    LanguageId = x.LanguageId,
                    LanguageName = x.Language.Name
                }).ToListAsync()
        };
    }

    public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
    {
        return await _signInManager.GetExternalLoginInfoAsync();
    }

    public async Task<string> GetGigSellerEmailAsync(int id)
    {
        var gig = await _repository.GetAllReadOnly<Gig>().Where(x => x.Id == id).FirstOrDefaultAsync();
        var seller = await _repository.GetAllReadOnly<Seller>().Where(x => x.Id == gig.SellerId).FirstOrDefaultAsync();
        return await _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == seller.UserId).Select(x => x.Email).FirstOrDefaultAsync();
    }

    public async Task<RoleFilterItemModel[]> GetRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles.Select(x => new RoleFilterItemModel()
        {
            Id = x.Id,
            Name = x.Name!
        }).ToArray();
    }

    public async Task<(string profilePictureUrl, string name, string username)> GetSellerDashboardInfoAsync(int sellerId)
    {
        var user = await GetCurrentApplicationUserAsync();

        return (user.ProfileImageUrl!, user.FullName!, user.UserName!);
    }

    public async Task<(string Industry, string MemberSince)> GetSellerDetailsForBuyer(int sellerId)
    {
        var seller = await _repository.GetAllReadOnly<Seller>().Include(x => x.Industry).FirstOrDefaultAsync(x => x.Id == sellerId);

        var user = await _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == seller.UserId).FirstOrDefaultAsync();
        return  (seller.Industry.Name, user.JoinedAt.ToString("MMMM dd, yyyy")) ;
    }

    public async Task<(string name, string profileImageUrl, bool topRated)> GetSellerDetailsForGigCardByIdAsync(int sellerId)
    {
        var seller = await _repository.GetAllReadOnly<Seller>().Where(x => x.Id == sellerId).FirstOrDefaultAsync();
        if(seller == null)
        {
            throw new NotFoundException(nameof(Seller), sellerId);
        }
        var user = await _userManager.FindByIdAsync(seller.UserId);
        if (user == null)
        {
            throw new NotFoundException("ApplicationUser", seller.UserId);
        }

        var reviews = await _repository.GetAllReadOnly<Review>().ToListAsync();
        var averageRating = reviews.Count > 0 ? reviews.Average(x => x.Rating) : 0;

        return (user.FullName, user.ProfileImageUrl, averageRating > 4.7);
    }

    public async Task<UserForAdminModel> GetSellerForGigAsync(int id)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(id);
        var seller = await _repository.GetAllReadOnly<Seller>().Include(x => x.Industry).FirstOrDefaultAsync(x => x.Id == gig.SellerId);
        var user = await _userManager.FindByIdAsync(seller.UserId);
        var country = await _repository.GetAllReadOnly<Address>().Where(x => x.UserId == seller.UserId).Include(x => x.Country).Select(x => x.Country.Name).FirstOrDefaultAsync();

        return new UserForAdminModel
        {
            Id = user.Id,
            Username = user.UserName,
            Roles = string.Join(", ", await _userManager.GetRolesAsync(user)),
            Joined = user.JoinedAt.ToString("MMMM dd, yyyy"),
            Country = country ?? string.Empty,
            Status = user.ModerationStatus.ToString()
        };      
    }

    public async Task<int> GetSellerIdByExistingsUserIdAsync(string userId)
    {
        var sellerId = await _repository.GetAllReadOnly<Seller>().Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync();
        if (sellerId == 0)
        {
            throw new NotFoundException(nameof(Seller), userId);
        }
        return sellerId;
    }

    public async Task<int> GetSellerIdByUserIdAsync()
    {
        var user = await GetCurrentUserAsync();
        var seller = await _repository.GetAllReadOnly<Seller>().FirstOrDefaultAsync(x => x.UserId == user.Id);
        if (seller == null)
        {
            throw new NotFoundException(nameof(Seller), user.Id);
        }
        return seller.Id;
    }

    public async Task<(string Id, string ProfilePictureUrl, string FullName, string Country, string Username, string Description)> GetSellerProfileDetails(string userId)
    {
        var user = await _repository.GetAllReadOnly<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == userId);
        
       return (userId, user.ProfileImageUrl, user.FullName, await GetUserCountryNameAsync(user.Id), user.UserName, user.Description);
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
        var user =  await _repository.GetAllReadOnly<ApplicationUser>().FirstOrDefaultAsync(x => x.RefreshToken == token);
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
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == emailOrUsername || x.Email == emailOrUsername) ;
        return user != null ? new ApplicationUserModel()
        {
            Id = user.Id,
            Email = user?.Email,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed,
        } : null;
    }

    public async Task<string> GetUserEmailByUserIdAsync(string userId)
    {
        var userEmail = await _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == userId).Select(x => x.Email).FirstOrDefaultAsync();
        if (userEmail == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), userId);
        }
        return userEmail;
    }

    public async Task<string> GetUserIdByBuyerIdAsync(int buyerId)
    {
        var userId = await _repository.GetAllReadOnly<Buyer>().Where(x => x.Id == buyerId).Select(x => x.UserId).FirstOrDefaultAsync();
        if (userId == null)
        {
            throw new NotFoundException(nameof(Buyer), buyerId);
        }
        return userId;
    }

    public async Task<string> GetUserIdBySellerIdAsync(int sellerId)
    {
        var userId = await _repository.GetAllReadOnly<Seller>().Where(x => x.Id == sellerId).Select(x => x.UserId).FirstOrDefaultAsync();
        if (userId == null)
        {
            throw new NotFoundException(nameof(Seller), sellerId);
        }
        return userId;
    }

    public Task<string> GetUserModerationStatusAsync(string? userId)
    {
        var moderationStatus = _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == userId).Select(x => x.ModerationStatus.ToString()).FirstOrDefaultAsync();
        if (moderationStatus == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), userId);
        }
        return moderationStatus;
    }

    public async Task<string> GetUsernameByBuyerIdAsync(int buyerId)
    {
        var userId = await _repository.GetAllReadOnly<Buyer>().Where(x => x.Id == buyerId).Select(x => x.UserId).FirstOrDefaultAsync();
        if (userId == null)
        {
            throw new NotFoundException(nameof(Buyer), buyerId);
        }
        return await _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefaultAsync();
    }

    public async Task<PaginatedResultModel<UserForAdminModel>> GetUsersForAdminAsync(SearchUsersQuery request)
    {
        var usersQueryable = _repository.GetAllReadOnly<ApplicationUser>().AsQueryable();
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            usersQueryable = usersQueryable.Where(x => x.UserName.Contains(request.Keyword) || x.Email.Contains(request.Keyword) || x.FullName.Contains(request.Keyword));
        }

        if(request.ModerationStatusId != null)
        {
            var moderationStatus = (QuickHire.Domain.Moderation.Enums.ModerationStatus)request.ModerationStatusId;
            usersQueryable = usersQueryable.Where(x => x.ModerationStatus == moderationStatus);
        }

        if(request.Id != null)
        {
            usersQueryable = usersQueryable.Where(x => x.Id == $"{request.Id}");
        }

        if (!string.IsNullOrEmpty(request.RoleId))
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            var userIdsInRole = (await _userManager.GetUsersInRoleAsync(role.Name)).Select(u => u.Id).ToList();

            usersQueryable = usersQueryable.Where(x => userIdsInRole.Contains(x.Id));
        }

        var totalCount = usersQueryable.Count();

        IEnumerable<ApplicationUser> userList;

        if (totalCount <= request.ItemsPerPage)
        {
            userList = await _repository.ToListAsync(usersQueryable.OrderBy(x => x.Id));
        }
        else
        {
            var pagedQuery = usersQueryable.Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage);

            userList = await _repository.ToListAsync(pagedQuery);
        }

        var userModels = new List<UserForAdminModel>();
        foreach (var user in userList)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userRoles = string.Join(", ", roles);
            var countryName = await GetUserCountryNameAsync(user.Id);

            userModels.Add(new UserForAdminModel
            {
                Id = user.Id,
                Joined = user.JoinedAt.ToString("MMMM dd, yyyy"),
                Username = user.UserName,
                Roles = userRoles,
                Country = countryName,
                Status = user.ModerationStatus.ToString()
            });
        }

        return new PaginatedResultModel<UserForAdminModel>()
        {
            Data = userModels,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.ItemsPerPage)
        };      
    }

    private async Task<string> GetUserCountryNameAsync(string id)
    {
        return await _repository.GetAllReadOnly<Address>() .Where(x => x.UserId == id).Include(x => x.Country).Select(x => x.Country.Name).FirstOrDefaultAsync() ?? string.Empty;
    }

    public async Task LogoutUserAsync(ApplicationUserModel user)
    {
        var appUser = await _repository.GetAll<ApplicationUser>().Where(x => x.Id == user.Id).FirstOrDefaultAsync();

        appUser.RefreshToken = null;
        appUser.RefreshTokenExpiresAt = null;

        await _repository.UpdateAsync(appUser);

        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete("ACCESS_TOKEN");
        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete("REFRESH_TOKEN");
    }

    public async Task<string> UpdateBuyerDetailsAsync(string description, IFormFile image)
    {
        var user = await GetCurrentApplicationUserAsync();
        user.Description = description;
        var imagePath = _cloudinaryService.UploadFile(image);
        if (imagePath == null)
        {
            throw new BadRequestException("Image upload failed", "Image upload failed.");
        }
        user.ProfileImageUrl = imagePath;

        await _repository.UpdateAsync(user);

        return imagePath;
    }

    public async Task<(string username, int userId, bool isSuccess)> UpdateCurrentUser(string? fullName, string? email, string? username, int? countryId, string? city, string? zipCode, string? street)
    {
        var userId = GetCurrentUserIdAsync();
        var user = await GetUserByIdAsync(userId);

        if(!string.IsNullOrEmpty(fullName))
        {
            user.FullName = fullName;
        }

        if (!string.IsNullOrEmpty(email))
        {
            user.Email = email;
        }

        if (!string.IsNullOrEmpty(username))
        {
            user.UserName = username;
        }

        var userAddress = await _repository.GetAllReadOnly<Address>().Where(x => x.UserId == user.Id).FirstOrDefaultAsync();

        if(userAddress == null)
        {
            var newAddress = new Address
            {
                CountryId = countryId.Value,
                City = city,
                ZipCode = zipCode,
                Street = street
            };

            await _repository.AddAsync(newAddress);
        }
        else
        {
            if (countryId.HasValue)
            {
                userAddress.CountryId = countryId.Value;
            }

            if (!string.IsNullOrEmpty(city))
            {
                userAddress.City = city;
            }

            if (!string.IsNullOrEmpty(zipCode))
            {
                userAddress.ZipCode = zipCode;
            }

            if (!string.IsNullOrEmpty(street))
            {
                userAddress.Street = street;
            }
        }
        user.ModerationStatus = Domain.Moderation.Enums.ModerationStatus.Active;

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        var buyerId = await GetBuyerIdByUserIdAsync();

        return (user.UserName, buyerId, true);
    }

    public async Task UpdateUserDescriptionAsync(string description)
    {
        var user = await GetCurrentApplicationUserAsync();
        user.Description = description;

        await _repository.UpdateAsync(user);
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
        return await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(nameof(ApplicationUser), userId);
    }

    public async Task<AboutUserModel> GetAboutCurrentUserAsync()
    {
        var user = await GetCurrentApplicationUserAsync();

        var rolesList = await _userManager.GetRolesAsync(user);
        var roles = rolesList.ToArray();

        var httpContextUser = _httpContextAccessor.HttpContext.User;

        var modeClaim = httpContextUser.FindFirst("mode")?.Value ?? "buyer"; 

        return new AboutUserModel
        {
            Id = user.Id,
            ProfilePictureUrl = user.ProfileImageUrl ?? string.Empty,
            Email = user.Email,
            Roles = roles,
            Mode = modeClaim, 
        };
    }

    public (string UserId, string Mode) GetCurrentUserIdAndMode()
    {
        var userId = GetCurrentUserIdAsync();
        var mode = _httpContextAccessor.HttpContext?.User.FindFirst("mode")?.Value ?? "buyer"; 
        return (userId, mode); 
    }

    public async Task<(string ProfilePictureUrl, string Username)> GetUsernameAndProfilePictureAsync(string participantBId)
    {
        var user =  await _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == participantBId).Select(x => new { x.ProfileImageUrl, x.UserName }).FirstOrDefaultAsync();

        return user != null ? (user.ProfileImageUrl ?? string.Empty, user.UserName) : (string.Empty, string.Empty);
    }

    public async Task<ParticipantBInfoModel> GetParticipantInfoAsync(string participantBId)
    {
        var user = await _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == participantBId).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), participantBId);
        }

        var userLanguages = await _repository.GetAllReadOnly<UserLanguage>().Where(x => x.UserId == participantBId).Include(x => x.Language).Select(x => x.Language.Name).ToArrayAsync();

        return new ParticipantBInfoModel
        {
               Id = user.Id,
            ProfilePictureUrl = user.ProfileImageUrl ?? string.Empty,
            FullName = user.FullName,
            Country = await _repository.GetAllReadOnly<Address>().Where(x => x.UserId == user.Id).Include(x => x.Country).Select(a => a.Country.Name).FirstOrDefaultAsync() ?? string.Empty,
            Username = user.UserName,
            Languages = userLanguages,
            MemberSince = user.JoinedAt.ToString("MMMM dd, yyyy")
        };           
    }

    public async Task<string> UpdateBuyerDescriptionAsync(string description)
    {
        var currentUserId = GetCurrentUserIdAsync();
        var user = _repository.GetAllReadOnly<ApplicationUser>().FirstOrDefault(x => x.Id == currentUserId);
        user.Description = description;

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        return user.ProfileImageUrl ?? string.Empty;
    }

    public async Task<string> GetUsernameByUserIdAsync(string userId)
    {
        return await _repository.GetAllReadOnly<ApplicationUser>().Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefaultAsync() ?? string.Empty;
    }

    public async Task ReportUserAsync(string reportedUserId)
    {
        var user = await _repository.GetAllReadOnly<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == reportedUserId);
        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), reportedUserId);
        }
        user.ModerationStatus = QuickHire.Domain.Moderation.Enums.ModerationStatus.PendingReview;

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task<int> CreateSellerAsync(int industryId, string username, string fullName, string description, IFormFile? profilePicture)
    {
        var currentUserId = GetCurrentUserIdAsync();
        var user = _repository.GetAllReadOnly<ApplicationUser>().FirstOrDefault(x => x.Id == currentUserId);
        user.UserName = username;
        user.FullName = fullName;
        user.Description = description;

        if (profilePicture != null)
        {
            var imagePath = _cloudinaryService.UploadFile(profilePicture);
            if (imagePath == null)
            {
                throw new BadRequestException("Image upload failed", "Image upload failed.");
            }
            user.ProfileImageUrl = imagePath;
        }

        await _repository.UpdateAsync(user);

        var seller = new Seller
        {
            UserId = user.Id,
            Clicks = 0,
            IndustryId = industryId
        };

        await _repository.AddAsync(seller);
        await _repository.SaveChangesAsync();

        await _userManager.AddToRoleAsync(user, QuickHire.Domain.Shared.Constants.UserRoles.Seller);

        return seller.Id;
    }

    public async Task<(string name, string address, string companyName)> GetBuyerForInvoiceAsync(string buyerUserId)
    {
        var billingDetails = await _repository.GetAllReadOnly<BillingDetails>().Include(x => x.Address.Country).Where(x => x.UserId == buyerUserId).FirstOrDefaultAsync();
        var billingAddress = string.Join(", ", billingDetails.Address.Street, billingDetails.Address.City, billingDetails.Address.ZipCode, billingDetails.Address.Country.Name);

        return (billingDetails.FullName, billingAddress, billingDetails.CompanyName);
    }

    public async Task<int> GetBuyerIdByExistingUserId(string userId)
    {
        return await _repository.GetAllReadOnly<Buyer>().Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync();
    }
}
