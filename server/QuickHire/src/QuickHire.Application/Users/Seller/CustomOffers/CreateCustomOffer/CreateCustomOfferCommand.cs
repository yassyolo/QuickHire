using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.CustomOffers;

namespace QuickHire.Application.Users.Seller.CustomOffers.CreateCustomOffer;

public record CreateCustomOfferCommand(int ProjectBriefId, int GigId, string Description, int DeliveryTime, decimal Total, int[] InclusivesIds) : ICommand<CustomOfferReturnModel>;
