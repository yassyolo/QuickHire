using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.RepeatBusiness.Cards;

public record GetRepeatBusinessStatisticsCardsQuery() : IQuery<IEnumerable<CardItemModel>>;

