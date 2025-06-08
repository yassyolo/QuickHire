using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.Engagement.Cards;

public record GetEngagementStatisticsCardsQuery() : IQuery<IEnumerable<CardItemModel>>;

