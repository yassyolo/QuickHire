using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Buyer.EditBuyerDetails;

public class EditBuyerCommandHandler : ICommandHandler<EditBuyerCommand, AddBuyerDetailsResponseModel>
{
    private readonly IUserService _userService;

    public EditBuyerCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<AddBuyerDetailsResponseModel> Handle(EditBuyerCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = string.Empty;
        if(request.Image != null)
        {
            imageUrl = await _userService.UpdateBuyerDetailsAsync(request.Description, request.Image);
        }
        else
        {
            imageUrl = await _userService.UpdateBuyerDescriptionAsync(request.Description);
        }

        return new AddBuyerDetailsResponseModel
        {
            ImageUrl = imageUrl
        };
    }
}
