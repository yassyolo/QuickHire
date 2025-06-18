using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.ProjectBriefs;
using QuickHire.Domain.ProjectBriefs.Enums;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace QuickHire.Application.ProjectBriefs.BuyerProjectBriefs;

public class GetBuyerProjectBriefsQueryHandler : IQueryHandler<GetBuyerProjectBriefsQuery, IEnumerable<BuyerProjectBriefModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetBuyerProjectBriefsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<BuyerProjectBriefModel>> Handle(GetBuyerProjectBriefsQuery request, CancellationToken cancellationToken)
    {
        var buyer = await _userService.GetBuyerIdByUserIdAsync();

        var projectBriefsQueryable = _repository.GetAllIncluding<Domain.ProjectBriefs.ProjectBrief>(x => x.CustomOffers).Where(x => x.BuyerId == buyer);

        if (request.FromDate != null)
        {
            var fromDate = DateTime.ParseExact(request.FromDate, "yyyy-MM-dd", null);
            projectBriefsQueryable = projectBriefsQueryable.Where(x => x.CreatedAt >= fromDate);
        }

        if (request.ToDate != null)
        {
            var toDate = DateTime.ParseExact(request.ToDate, "yyyy-MM-dd", null);
            projectBriefsQueryable = projectBriefsQueryable.Where(x => x.CreatedAt <= toDate);
        }

        if (request.Keyword != null && request.Keyword.Trim().Length > 0)
        {
            var keyword = request.Keyword.Trim();
            projectBriefsQueryable = projectBriefsQueryable.Where(x => x.ProjectBriefNumber.ToLower().Contains(keyword) || x.Description.ToLower().Contains(keyword) || x.AboutBuyer.ToLower().Contains(keyword));
        }
        var projectBriefsList = await _repository.ToListAsync(projectBriefsQueryable);
        var projectBriefsIds = projectBriefsList.OrderByDescending(x => x.CreatedAt).Select(x => x.Id).ToList();

        var suitableSellersQueryable = _repository.GetAllReadOnly<Domain.ProjectBriefs.SuitableSellerProjectBrief>().Where(x => projectBriefsIds.Contains(x.ProjectBriefId));
        var suitableSellersList = await _repository.ToListAsync(suitableSellersQueryable);

        var sellersReachedByBrief = suitableSellersList.GroupBy(x => x.ProjectBriefId).ToDictionary(x => x.Key, x => x.Count());

        return projectBriefsList.Select(x => new BuyerProjectBriefModel
        {
            Id = x.Id,
            Date = x.CreatedAt.ToString("dd MMM yyyy"),
            DocumentNumber = x.ProjectBriefNumber,
            SellersReached = sellersReachedByBrief.TryGetValue(x.Id, out var count) ? count : 0,
            TotalOffers = x.CustomOffers.Count(),
            Order = x.Status == ProjectBriefStatus.OrderPlaced ? "yes" : "no",
            Status = x.Status.ToString()
        });
    }
}

