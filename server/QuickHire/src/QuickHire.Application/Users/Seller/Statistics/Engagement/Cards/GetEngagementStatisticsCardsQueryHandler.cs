using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.Engagement.Cards;

public class GetEngagementStatisticsCardsQueryHandler : IQueryHandler<GetEngagementStatisticsCardsQuery, IEnumerable<CardItemModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetEngagementStatisticsCardsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<IEnumerable<CardItemModel>> Handle(GetEngagementStatisticsCardsQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var seller = await _repository.GetByIdAsync<QuickHire.Domain.Users.Seller, int>(sellerId);
        var gigsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Gigs.Gig>().Where(x => x.SellerId == sellerId);
        var gigs = await _repository.ToListAsync<QuickHire.Domain.Gigs.Gig>(gigsQueryable);
        var gigIds = gigs.Select(x => x.Id).ToList();

        var browsingHistoryQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.BrowsingHistory>().Where(x => gigIds.Contains(x.GigId));
        var browsingHistory = await _repository.ToListAsync<QuickHire.Domain.Users.BrowsingHistory>(browsingHistoryQueryable);

        var favouriteGigsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGig>().Where(x => gigIds.Contains(x.GigId));
        var favouriteGigs = await _repository.ToListAsync<QuickHire.Domain.Users.FavouriteGig>(favouriteGigsQueryable);

        var suitableProjectBriefsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.ProjectBriefs.SuitableSellerProjectBrief>().Where(x => x.SellerId == sellerId);
        var suitableProjectBriefs = await _repository.ToListAsync<QuickHire.Domain.ProjectBriefs.SuitableSellerProjectBrief>(suitableProjectBriefsQueryable);

        var conversationsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Messaging.Conversation>().Where(x => x.SellerId == sellerId);
        var conversations = await _repository.ToListAsync<QuickHire.Domain.Messaging.Conversation>(conversationsQueryable);
        var conversationIds = conversations.Select(x => x.Id).ToList();
        var firstMessagesReceivedBySellerQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Messaging.Message>().Where(x => x.ReceiverId == sellerId && conversationIds.Contains(x.ConversationId))
                                                    .GroupBy(x => x.ConversationId).Select(x => x.OrderBy(x => x.SentAt).FirstOrDefault());

        var firstMessagesReceivedBySeller = await _repository.ToListAsync<QuickHire.Domain.Messaging.Message>(firstMessagesReceivedBySellerQueryable);

        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Profile views",
                Value = seller.Clicks.ToString()
            },
            new CardItemModel
            {
                Title = "Gig Clicks",
                Value = browsingHistory.Count().ToString()
            },
            new CardItemModel
            {
                Title = "Message Initiations",
                Value = firstMessagesReceivedBySeller.Count().ToString()
            },
            new CardItemModel
            {
                Title = "Gig Saves",
                Value = favouriteGigs.Count().ToString()
            },
            new CardItemModel
            {
                Title = "Project Briefs",
                Value = suitableProjectBriefs.Count().ToString()
            }
        };*/

        return new List<CardItemModel>
        {
            new CardItemModel
            {
                Title = "Profile views",
                Value = "1200"
            },
            new CardItemModel
            {
                Title = "Gig views",
                Value = "850"
            },
            new CardItemModel
            {
                Title = "Message initiations",
                Value = "130"
            },
            new CardItemModel
            {
                Title = "Gig saves",
                Value = "320"
            },
            new CardItemModel
            {
                Title = "Received project briefs",
                Value = "27"
            }
        };
    }
}
