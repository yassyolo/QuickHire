using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.OrderFullfillment.Cards;

public record GetOrderFullfillmentStatisticsCradsQuery() : IQuery<IEnumerable<CardItemModel>>;
