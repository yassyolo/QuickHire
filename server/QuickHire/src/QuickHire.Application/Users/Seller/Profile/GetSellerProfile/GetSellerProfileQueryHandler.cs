using Microsoft.Extensions.Primitives;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Application.Users.Seller.Profile.GetSellerProfile;

public class GetSellerProfileQueryHandler : IQueryHandler<GetSellerProfileQuery, SellerProfileModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetSellerProfileQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<SellerProfileModel> Handle(GetSellerProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetCurrentUserIdAsync();
        var sellerUser = await _userService.GetSellerProfileDetails(userId);
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var userLanguageQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.UserLanguage>(x => x.Language).Where(x => x.UserId == sellerUser.Id);
        var userLanguageList = await _repository.ToListAsync<QuickHire.Domain.Users.UserLanguage>(userLanguageQueryable);
        var userLanguageModel = userLanguageList.Select(x => new UserLanguageModel
        {
            LanguageId = x.LanguageId,
            LanguageName = x.Language.Name,
        }).ToList();

        var skillsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.Skill>().Where(x => x.SellerId == sellerId);
        var skillsList = await _repository.ToListAsync<QuickHire.Domain.Users.Skill>(skillsQueryable);
        var skillsModel = skillsList.Select(x => new SkillModel
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();

        var educationQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.Education>().Where(x => x.SellerId == sellerId);
        var educationList = await _repository.ToListAsync<QuickHire.Domain.Users.Education>(educationQueryable);
        var educationModel = educationList.Select(x => new EducationModel
        {
            Id = x.Id,
            Major = x.Major,
            Institution = x.Institution,
            EndYear = x.GraduationYear.ToString(),
            Degree = x.Degree.ToString()
        }).ToList();

        var certificationQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.Certification>().Where(x => x.SellerId == sellerId);
        var certificationList = await _repository.ToListAsync<QuickHire.Domain.Users.Certification>(certificationQueryable);
        var certificationModel = certificationList.Select(x => new CertificationModel
        {
            Id = x.Id,
            Certification = x.Name,
            Issuer = x.Issuer,
            Date = x.IssuedAt.ToString("dd-MM-yyyy")        
        }).ToList();

        var portfolioQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.Portfolio>(x => x.MainCategory).Where(x => x.SellerId == sellerId);
        var portfolioList = await _repository.ToListAsync<QuickHire.Domain.Users.Portfolio>(portfolioQueryable);
        var portfolioModel = portfolioList.Select(x => new PortfolioModel
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            MainCategoryId = x.MainCategoryId,
            MainCategoryName = x.MainCategory.Name
        }).ToList();

        return new SellerProfileModel
        {
            ProfilePictureUrl = sellerUser.ProfilePictureUrl,
            FullName = sellerUser.FullName,
            Country = sellerUser.Country,
            Username = sellerUser.Username,
            Description = sellerUser.Description,
            Languages = userLanguageModel,
            Skills = skillsModel,
            Education = educationModel,
            Certifications = certificationModel,
            Portfolios = portfolioModel,

        };
    }
}

