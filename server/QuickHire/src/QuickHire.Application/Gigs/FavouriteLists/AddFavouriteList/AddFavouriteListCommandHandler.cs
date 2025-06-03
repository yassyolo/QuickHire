using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength;
using FavouriteGigsList = QuickHire.Domain.Users.FavouriteGigsList;

namespace QuickHire.Application.Gigs.FavouriteLists.AddFavouriteList;

public class AddFavouriteListCommandHandler : ICommandHandler<AddFavouriteListCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public AddFavouriteListCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(AddFavouriteListCommand request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();

        var favouriteList = new FavouriteGigsList
        {
            Name = request.Name,
            BuyerId = buyerId,
            CreatedAt = DateTime.Now,
            Description = request.Description
        };

        await _repository.AddAsync(favouriteList);
        await _repository.SaveChangesAsync();

        if (request.GigId.HasValue)
        {
            var gig = await _repository.GetByIdAsync<QuickHire.Domain.Gigs.Gig, int>(request.GigId.Value);
            if (gig == null)
            {
                throw new NotFoundException(nameof(QuickHire.Domain.Gigs.Gig), request.GigId);
            }

            var newFavouriteGig = new QuickHire.Domain.Users.FavouriteGig
            {
                BuyerId = buyerId,
                GigId = gig.Id,
                FavouriteGigsListId = favouriteList.Id,
                AddedAt = DateTime.Now
            };

            await _repository.AddAsync(newFavouriteGig);
            await _repository.SaveChangesAsync();
        }

        return Unit.Value;
    }
}

