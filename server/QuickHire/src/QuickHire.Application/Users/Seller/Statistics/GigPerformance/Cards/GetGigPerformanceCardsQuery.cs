using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.GigPerformance.Cards;

public record GetGigPerformanceCardsQuery() : IQuery<IEnumerable<CardItemModel>>;

