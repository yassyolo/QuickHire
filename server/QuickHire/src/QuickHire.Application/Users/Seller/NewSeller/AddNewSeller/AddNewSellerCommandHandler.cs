using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength;

namespace QuickHire.Application.Users.Seller.NewSeller.AddNewSeller;

public class AddNewSellerCommandHandler : ICommandHandler<AddNewSellerCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public AddNewSellerCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(AddNewSellerCommand request, CancellationToken cancellationToken)
    {
       var sellerId = await _userService.CreateSellerAsync(request.IndustryId, request.Username, request.FullName, request.Description, request.ProfilePicture);
       foreach (var certification in request.Certifications)
        {
            var newCertification = new Domain.Users.Certification
            {
                SellerId = sellerId,
                Name = certification.Certification,
                Issuer = certification.Issuer,
                IssuedAt = DateTime.Parse(certification.Date),
            };
            await _repository.AddAsync(newCertification);
        }

       foreach( var education in request.Educations )
        {
            var newEducation = new Domain.Users.Education
            {
                Institution = education.Institution,
                Degree = education.Degree,
                GraduationYear = int.Parse(education.EndYear),
                Major = education.Major,
                SellerId = sellerId
            };
            await _repository.AddAsync(newEducation);
        }

       foreach (var skill in request.Skills)
        {
            var newSkill = new Domain.Users.Skill()
            {
                Name = skill.Name,
                SellerId = sellerId
            };
            await _repository.AddAsync(newSkill);
        }

        var userId = _userService.GetCurrentUserIdAsync();

        var userLanguagesQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.UserLanguage>().Where(x => x.UserId == userId);
        var existingUserLanguages = await _repository.ToListAsync(userLanguagesQueryable);

        var existingLanguageIds = existingUserLanguages.Select(x => x.LanguageId).ToList();

        var languagesToRemove = existingUserLanguages.Where(x => !request.Languages.Contains(x.LanguageId)).ToList();

        foreach (var language in languagesToRemove)
        {
            await _repository.DeleteAsync(language);
        }

        var languagesToAdd = request.Languages.Where(x => !existingLanguageIds.Contains(x));
        foreach (var languageId in languagesToAdd)
        {
            var newLanguage = new QuickHire.Domain.Users.UserLanguage
            {
                UserId = userId,
                LanguageId = languageId
            };
            await _repository.AddAsync(newLanguage);
        }


        await _repository.SaveChangesAsync();

       return Unit.Value;
    }
}
