using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.Profile.EditLanguages;

public class EditLanguagesCommandHandler : ICommandHandler<EditLanguagesCommand, List<UserLanguageModel>>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;

    public EditLanguagesCommandHandler(IUserService userService, IRepository repository)
    {
        _userService = userService;
        _repository = repository;
    }
    public async Task<List<UserLanguageModel>> Handle(EditLanguagesCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetCurrentUserIdAsync();

        var userLanguagesQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.UserLanguage>().Where(x => x.UserId == userId);
        var existingUserLanguages = await _repository.ToListAsync(userLanguagesQueryable);

        var existingLanguageIds = existingUserLanguages.Select(x => x.LanguageId).ToList();
        var newLanguageIds = request.Languages.Select(x => x.LanguageId).ToList();

        var languagesToRemove = existingUserLanguages.Where(x => !newLanguageIds.Contains(x.LanguageId)).ToList();

        foreach (var language in languagesToRemove)
        {
            await _repository.DeleteAsync(language);
        }

        var languagesToAdd = newLanguageIds.Where(x => !existingLanguageIds.Contains(x));
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

        var updatedUserLanguagesQueryable =  _repository.GetAllReadOnly<QuickHire.Domain.Users.UserLanguage>().Where(x => x.UserId == userId);
        var updatedUserLanguages = await _repository.ToListAsync(updatedUserLanguagesQueryable);

        return updatedUserLanguages.Select(x => new UserLanguageModel
        {
            LanguageId = x.LanguageId,
            LanguageName = x.Language.Name,
        }).ToList();
    }
}
