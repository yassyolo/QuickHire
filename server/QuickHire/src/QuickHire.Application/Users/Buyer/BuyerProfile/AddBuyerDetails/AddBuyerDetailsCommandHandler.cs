using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Buyer.BuyerProfile.AddBuyerDetails;

public class AddBuyerDetailsCommandHandler : ICommandHandler<AddBuyerDetailsCommand, AddBuyerDetailsResponseModel>
{
    private readonly IUserService _userService;

    public AddBuyerDetailsCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<AddBuyerDetailsResponseModel> Handle(AddBuyerDetailsCommand request, CancellationToken cancellationToken)
    {
        var updatedBuyerDetails = await _userService.UpdateBuyerDetailsAsync(request.Description, request.Image);

        return new AddBuyerDetailsResponseModel
        {
            ImageUrl = updatedBuyerDetails
        };
    }

}

