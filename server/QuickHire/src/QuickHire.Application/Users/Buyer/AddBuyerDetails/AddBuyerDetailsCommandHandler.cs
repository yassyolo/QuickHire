using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Buyer.AddBuyerDetails;

public class AddBuyerDetailsCommandHandler : ICommandHandler<AddBuyerDetailsCommand, AddBuyerDetailsResponseModel>
{
    private readonly IRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUserService _userService;

    public AddBuyerDetailsCommandHandler(IRepository repository, ICloudinaryService cloudinaryService, IUserService userService)
    {
        _repository = repository;
        _cloudinaryService = cloudinaryService;
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

