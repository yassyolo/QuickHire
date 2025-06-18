using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.CustomOffers;

namespace QuickHire.Application.CustomOffers.GetCustomOffer;

public record GetCustomOfferQuery(int Id) : IQuery<CustomOfferPreviewModel>;
