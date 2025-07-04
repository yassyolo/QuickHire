﻿using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.CustomOffers;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.CustomOffers.GetCustomOffer;

public class GetCustomOfferQueryHandler : IQueryHandler<GetCustomOfferQuery, CustomOfferPreviewModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetCustomOfferQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<CustomOfferPreviewModel> Handle(GetCustomOfferQuery request, CancellationToken cancellationToken)
    {
        var customOffer = await _repository.GetByIdAsync<Domain.CustomOffers.CustomOffer, int>(request.Id)!;
        if (customOffer == null)
        {
            throw new NotFoundException(nameof(customOffer), request.Id);
        }

        var offerIncludesQueryable = _repository.GetAllIncluding<Domain.CustomOffers.CustomOfferInclusives>(x => x.PaymentPlanInclude).Where(x => x.CustomOfferId == customOffer.Id);
        var offerIncludesList = await _repository.ToListAsync(offerIncludesQueryable);
        var userId = await _userService.GetUserIdBySellerIdAsync(customOffer.SellerId);
        var userInfo = await _userService.GetUserInfoForPreviewAsync(userId);


        return new CustomOfferPreviewModel
        {
            CustomOfferNumber = customOffer.CustomOfferNumber,
            Description = customOffer.Description,
            Price = customOffer.Price.ToString(),
            Revisions = customOffer.Revisions.ToString(),
            DeliveryTimeInDays = customOffer.DeliveryTimeInDays.ToString(),
            OfferIncludes = offerIncludesList.Select(x => x.PaymentPlanInclude.Name).ToArray(),
            CreateOn = customOffer.CreatedAt.ToString("dd MMM, yyyy"),
            Status = customOffer.Status.ToString(),
            SellerName = userInfo.Name,
            SellerProfilePictureUrl = userInfo.ProfilePictureUrl,
            MemberSince = userInfo.MemberSince,
            Location = userInfo.Location,
            Languages = userInfo.Languages
        };
    }
}
