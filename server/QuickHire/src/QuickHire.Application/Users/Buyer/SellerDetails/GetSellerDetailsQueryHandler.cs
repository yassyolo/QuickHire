using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Application.Users.Models.SellerDetails;

namespace QuickHire.Application.Users.Buyer.SellerDetails;

public class GetSellerDetailsQueryHandler : IQueryHandler<GetSellerDetailsQuery, SellerDetailsForBuyerModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetSellerDetailsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    
    public async Task<SellerDetailsForBuyerModel> Handle(GetSellerDetailsQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = 0;
        var userId = string.Empty;
        if (request.Id.HasValue)
        {
            sellerId = request.Id.Value;
            userId = await _userService.GetUserIdBySellerIdAsync(sellerId);
        }

        if (!string.IsNullOrEmpty(request.UserId))
        {
            sellerId = await _userService.GetSellerIdByExistingsUserIdAsync(request.UserId);
            userId = request.UserId;
        }

        var sellerUser = await _userService.GetSellerProfileDetails(userId);

        var userLanguageQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.UserLanguage>().Where(x => x.UserId == sellerUser.Id);
        userLanguageQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.UserLanguage>(x => x.Language);
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

        var educationQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.Education>().Where(x => x.SellerId == sellerId);
        educationQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.Education>(x => x.Degree);
        var educationList = await _repository.ToListAsync<QuickHire.Domain.Users.Education>(educationQueryable);
        var educationModel = educationList.Select(x => new EducationModel
        {
            Id = x.Id,
            Major = x.Major,
            Institution = x.Institution,
            EndYear = x.GraduationYear.ToString("dd-MM-yyyy"),
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

        var portfolioQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.Portfolio>().Where(x => x.SellerId == sellerId);
        portfolioQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.Portfolio>(x => x.MainCategory);
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

        var ordersQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Order>().Where(x => x.SellerId == sellerId);
        ordersQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Order>(x => x.Reviews);
        var ordersList = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(ordersQueryable);

        var averageRating = ordersList.Any() ? ordersList.Average(x => x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 0) : 0;
        var topRated = ordersList.Any() ? ordersList.All(x => x.Reviews.Any() && x.Reviews.Average(r => r.Rating) >= 4.5) : false;

        var totalReviews = ordersList.Sum(x => x.Reviews.Count());


        var sellerDetailsForBuyer = await _userService.GetSellerDetailsForBuyer(sellerId);

        return new SellerDetailsForBuyerModel
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
        AverageRating = averageRating,
        TopRated = topRated,
        Industry = sellerDetailsForBuyer.Industry,
        MemberSince = sellerDetailsForBuyer.MemberSince,
        TotalReviews = totalReviews

        };*/

        return new SellerDetailsForBuyerModel
        {
            ProfilePictureUrl = "https://example.com/profile.jpg",
            FullName = "John Doe",
            Country = "USA",
            Username = "johndoe",
            Description = "Experienced software developer specializing in web applications.",
            Languages = new List<UserLanguageModel>
            {
                new UserLanguageModel { LanguageId = 1, LanguageName = "English" },
                new UserLanguageModel {LanguageId = 2, LanguageName = "Spanish" }
            },
            Skills = new List<SkillModel>
            {
                new SkillModel { Id = 1, Name = "C#" },
                new SkillModel { Id = 2, Name = "JavaScript" }
            },
            Education = new List<EducationModel>
            {
                new EducationModel { Id = 1, Major = "Computer Science", Institution = "University A", EndYear = "2020-05-15", Degree = "Bachelor" }
            },
            Certifications = new List<CertificationModel>
            {
                new CertificationModel { Id = 1, Certification = "Certified .NET Developer", Issuer = "Microsoft", Date = "2021-06-01" }
            },
            Portfolios = new List<PortfolioModel>
            {
                new PortfolioModel { Id = 1, Title = "Portfolio Project 1", Description = "Description of project 1", ImageUrl = "https://picsum.photos/200/300", MainCategoryId = 1, MainCategoryName = "category1" },
                new PortfolioModel { Id = 2, Title = "Portfolio Project 2", Description = "Description of project 2", ImageUrl = "https://picsum.photos/200/300", MainCategoryId = 1, MainCategoryName = "category1" }
            },
            AverageRating = 4.5,
            TopRated = true,
            Industry = "Software Development",
            MemberSince = "2020-01-01",
            TotalReviews = 100

        };
    }
}

