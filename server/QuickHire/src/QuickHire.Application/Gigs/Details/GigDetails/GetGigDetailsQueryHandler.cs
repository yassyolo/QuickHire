using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Details;
using QuickHire.Domain.Shared.Exceptions;
using System.Security.Cryptography.X509Certificates;

namespace QuickHire.Application.Gigs.Details.GigDetails;

public class GetGigDetailsQueryHandler : IQueryHandler<GetGigDetailsQuery, GigDetailsModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetGigDetailsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    

    public async Task<GigDetailsModel> Handle(GetGigDetailsQuery request, CancellationToken cancellationToken)
    {
        var gigsQueryable = _repository.GetAllIncluding<QuickHire.Domain.Gigs.Gig>(x => x.SubSubCategory.SubCategory.MainCategory,  x => x.PaymentPlans, x => x.Metadata,  x => x.Orders, x => x.Seller).Where(x => x.Id == request.Id);
        var gig = await _repository.FirstOrDefaultAsync(gigsQueryable);
        if (gig == null)
        {
            throw new NotFoundException(nameof(QuickHire.Domain.Gigs.Gig), request.Id);
        }
        var liked = false;

        if (!request.Preview)
        {
            var buyerId = await _userService.GetBuyerIdByUserIdAsync();
            var browsingHistoryQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.BrowsingHistory>(x => x.Gig).Where(x => x.GigId == gig.Id && x.BuyerId == buyerId);
            var browsingHistory = await _repository.FirstOrDefaultAsync(browsingHistoryQueryable);
            var gigSellerUserId = await _userService.GetUserIdBySellerIdAsync(gig.SellerId);
            var buyerUserId = await _userService.GetUserIdByBuyerIdAsync(buyerId);

            if (browsingHistory == null)
            {
                if (gigSellerUserId != buyerUserId)
                {
                    browsingHistory = new QuickHire.Domain.Users.BrowsingHistory
                    {
                        GigId = gig.Id,
                        BuyerId = buyerId,
                        ViewedAt = DateTime.Now
                    };
                    await _repository.AddAsync(browsingHistory);
                    await _repository.SaveChangesAsync();
                }
            }
            else
            {
                if (gigSellerUserId != buyerUserId)
                {
                    browsingHistory.ViewedAt = DateTime.Now;
                    await _repository.UpdateAsync(browsingHistory);
                    await _repository.SaveChangesAsync();
                }
            }

            var favouriteGigsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGig>().Where(x => x.GigId == gig.Id && x.BuyerId == buyerId);
            var favouriteGig = await _repository.FirstOrDefaultAsync(favouriteGigsQueryable);
            if (favouriteGig != null)
            {
                liked = true;
            }
        }

        var model = new GigDetailsModel
        {
            MainCategoryId = gig.SubSubCategory.SubCategory.MainCategory.Id,
            SubCategoryId = gig.SubSubCategory.SubCategory.Id,
            MainCategoryName = gig.SubSubCategory.SubCategory.MainCategory.Name,
            SubCategoryName = gig.SubSubCategory.SubCategory.Name,
            UserId = await _userService.GetUserIdBySellerIdAsync(gig.SellerId),
            Title = gig.Title,
            Description = gig.Description,
            ImageUrls = gig.ImageUrls.ToArray()  ,
            Liked = liked,
        };

        var paymentPlansQueryable = _repository.GetAllIncluding<QuickHire.Domain.Gigs.PaymentPlan>(x => x.Inclusions).Where(x => x.GigId == gig.Id);
        var paymentPlans = await _repository.ToListAsync(paymentPlansQueryable);
        model.PaymentPlans = paymentPlans.Select(x => new QuickHire.Application.Gigs.Models.Details.PaymentPlanModel
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Description = x.Description,
            DeliveryTimeInDays = x.DeliveryTimeInDays,
            Revisions = x.Revisions,
            Inclusions = x.Inclusions.Select(x => new QuickHire.Application.Gigs.Models.Details.PaymentPlanIncludeModel
            {
                Name = x.Name,
                Value = x.Value
            }).ToList()
        }).ToArray();


        var gigMetadataQueryable = _repository.GetAllIncluding<QuickHire.Domain.Gigs.GigMetadata>(x => x.FilterOption.GigFilter).Where(x => x.GigId == gig.Id && x.FilterOption.GigFilter.Type != Domain.Categories.Enums.GigFilterType.DeliveryTime && x.FilterOption.GigFilter.Type != Domain.Categories.Enums.GigFilterType.PriceRange);
        var gigMetadata = await _repository.ToListAsync(gigMetadataQueryable);
        var filterOptionIds = gigMetadata.Select(x => x.FilterOptionId).ToList();
        var filterOptionsQueryable = _repository.GetAllIncluding<QuickHire.Domain.Categories.FilterOption>(x => x.GigFilter).Where(x => filterOptionIds.Contains(x.Id));
        var filterOptions = await _repository.ToListAsync(filterOptionsQueryable);
        var groupedOptions = filterOptions
    .GroupBy(x => x.GigFilter) 
    .Select(x => new GigMetaDataModel
    {
        Title = x.Key.Title,
        Items = x.Select(x => x.Name).ToArray()
    }).ToArray();

        model.GigMetaData = groupedOptions;

        var favouritesQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGig>().Where(x => x.GigId == gig.Id);
        var favourites = await _repository.ToListAsync(favouritesQueryable);

        if (request.Preview)
        {
            model.OrdersInQueue = gig.Orders.Count();
            model.NumberOfLikes = favourites.Count();
            model.SellerId = gig.Seller.Id;
        }

        return model;
    }
}
