using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Seller.Profile.EditDescription;

public record EditDescriptionCommand(string Description) : ICommand<Unit>;