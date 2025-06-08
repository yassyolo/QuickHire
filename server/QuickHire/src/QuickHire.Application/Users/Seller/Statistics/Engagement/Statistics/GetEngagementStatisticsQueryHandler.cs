using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Statistics;
using QuickHire.Application.Users.Seller.Statistics.Engagement.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Seller.Statistics.Engagement.Data;

public class GetEngagementStatisticsQueryHandler: IQueryHandler<GetEngagementStatisticsQuery, IEnumerable<EngagementStatisticsRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetEngagementStatisticsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<EngagementStatisticsRowModel>> Handle( GetEngagementStatisticsQuery request,CancellationToken cancellationToken)
    {
        /*var (startDate, endDate) = ParseRange(request.Range ?? "last 30 days");
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var seller = await _repository.GetByIdAsync<QuickHire.Domain.Users.Seller, int>(sellerId);

        var gigsQueryable = _repository.GetAllReadOnly<Gig>().Where(x => x.SellerId == sellerId);
        var gigs = await _repository.ToListAsync(gigsQueryable);
        var gigIds = gigs.Select(x => x.Id).ToList();

        var browsingHistoryQueryable = _repository.GetAllReadOnly<BrowsingHistory>().Where(x => x.SellerId == sellerId && x.ViewedAt.Date >= startDate && x.ViewedAt.Date <= endDate);
        var browsingHistory = await _repository.ToListAsync(browsingHistoryQueryable);

        var favouriteGigsQueryable = _repository.GetAllReadOnly<FavouriteGig>()
                .Where(x => gigIds.Contains(x.GigId) && x.AddedAt.Date >= startDate && x.AddedAt.Date <= endDate);

        var favouriteGigs = await _repository.ToListAsync(favouriteGigsQueryable);

        var projectBriefsQueryable = _repository.GetAllReadOnly<SuitableSellerProjectBrief>().Where(x => x.SellerId == sellerId);
        projectBriefsQueryable = _repository.GetAllIncluding<SuitableSellerProjectBrief>(x => x.ProjectBrief);
        projectBriefsQueryable = projectBriefsQueryable.Where(x => x.ProjectBrief.CreatedAt.Date >= startDate && x.ProjectBrief.CreatedAt.Date <= endDate);
        var briefs = await _repository.ToListAsync(projectBriefsQueryable);

        var conversationsQueryable = _repository.GetAllReadOnly<Conversation>().Where(c => c.SellerId == sellerId);
        var conversations = await _repository.ToListAsync(conversationsQueryable);
        var conversationIds = conversations.Select(c => c.Id).ToList();

        var messagesQueryable = _repository.GetAllReadOnly<Message>()
            .Where(x => x.ReceiverId == sellerId && conversationIds.Contains(x.ConversationId) && x.SentAt.Date >= startDate && x.SentAt.Date <= endDate);
        var allMessages = await _repository.ToListAsync(messagesQueryable);

        var firstMessages = allMessages.GroupBy(x => x.ConversationId).Select(x => x.OrderBy(x => x.SentAt).First()).ToList();

        var dateRange = Enumerable.Range(0, (endDate - startDate).Days + 1)
            .Select(x => startDate.AddDays(x))
            .ToList();

        var result = dateRange.Select(date =>
        {
            var dateOnly = date.Date;

            return new EngagementStatisticsRowModel
            {
                Date = dateOnly.ToString("yyyy-MM-dd"),
                ProfileViews = browsingHistory.Count(x => x.ViewedAt.Date == dateOnly), 
                GigClicks = browsingHistory.Count(b => b.ViewedAt.Date == dateOnly),
                MessagesStarted = firstMessages.Count(m => m.SentAt.Date == dateOnly),
                GigSaves = favouriteGigs.Count(f => f.AddedAt.Date == dateOnly),
                BriefsReceived = briefs.Count(b => b.ProjectBrief.CreatedAt.Date == dateOnly)
            };
        });

        return result;*/

        return new List<EngagementStatisticsRowModel>
        {
            new EngagementStatisticsRowModel
            {
                Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                ProfileViews = 10,
                GigClicks = 5,
                MessagesStarted = 2,
                GigSaves = 3,
                BriefsReceived = 1
            },
            new EngagementStatisticsRowModel
            {
                Date = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd"),
                ProfileViews = 8,
                GigClicks = 4,
                MessagesStarted = 1,
                GigSaves = 2,
                BriefsReceived = 0
            },
            new EngagementStatisticsRowModel
            {
                Date = DateTime.UtcNow.AddDays(-2).ToString("yyyy-MM-dd"),
                ProfileViews = 12,
                GigClicks = 6,
                MessagesStarted = 3,
                GigSaves = 4,
                BriefsReceived = 2
            },
            new EngagementStatisticsRowModel
            {
                Date = DateTime.UtcNow.AddDays(-3).ToString("yyyy-MM-dd"),
                ProfileViews = 15,
                GigClicks = 7,
                MessagesStarted = 4,
                GigSaves = 5,
                BriefsReceived = 3
            },
            new EngagementStatisticsRowModel
            {
                Date = DateTime.UtcNow.AddDays(-4).ToString("yyyy-MM-dd"),
                ProfileViews = 20,
                GigClicks = 10,
                MessagesStarted = 5,
                GigSaves = 6,
                BriefsReceived = 4
            }

        };
    }

    private (DateTime Start, DateTime End) ParseRange(string range)
    {
        var today = DateTime.UtcNow.Date;

        return range.Trim().ToLower() switch
        {
            "last 30 days" => (today.AddDays(-29), today),
            "last 3 months" => (today.AddMonths(-3), today),
            "yearly" => (new DateTime(today.Year, 1, 1), today),
            _ => (today.AddDays(-29), today)
        };
    }
}
