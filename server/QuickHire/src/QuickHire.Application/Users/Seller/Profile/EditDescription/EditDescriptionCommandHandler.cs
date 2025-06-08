using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using System.Runtime.CompilerServices;

namespace QuickHire.Application.Users.Seller.Profile.EditDescription;

public class EditDescriptionCommandHandler : ICommandHandler<EditDescriptionCommand, Unit>
{
    private readonly IUserService _userService;

    public EditDescriptionCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<Unit> Handle(EditDescriptionCommand request, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserDescriptionAsync(request.Description);

        return Unit.Value;
    }
}
