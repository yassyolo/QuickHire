using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.Earnings.Cards;

public class GetEarningStatisticsCardsQuery() : IQuery<IEnumerable<CardItemModel>>;

