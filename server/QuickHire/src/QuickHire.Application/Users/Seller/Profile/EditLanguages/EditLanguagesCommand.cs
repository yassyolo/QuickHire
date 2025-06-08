using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;
using System.Windows.Input;

namespace QuickHire.Application.Users.Seller.Profile.EditLanguages;

public record EditLanguagesCommand(List<LangugesToEditModel> Languages) : ICommand<List<UserLanguageModel>>;
