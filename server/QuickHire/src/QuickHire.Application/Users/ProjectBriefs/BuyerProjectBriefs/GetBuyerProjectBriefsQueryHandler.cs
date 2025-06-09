using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.ProjectBriefs;
using QuickHire.Domain.ProjectBriefs.Enums;

namespace QuickHire.Application.Users.ProjectBriefs.BuyerProjectBriefs;

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
        /*var buyer = await _userService.GetBuyerIdByUserIdAsync();

        var projectBriefsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.ProjectBriefs.ProjectBrief>().Where(x => x.BuyerId == buyer);
        projectBriefsQueryable = _repository.GetAllIncluding<QuickHire.Domain.ProjectBriefs.ProjectBrief>(x => x.CustomOffers);

        if (request.FromDate != null && request.ToDate != null)
        {
            var fromDate = DateTime.ParseExact(request.FromDate, "yyyy-MM-dd", null);
            projectBriefsQueryable = projectBriefsQueryable.Where(x => x.CreatedAt >= fromDate);
        }

        if (request.ToDate != null && request.FromDate != null)
        {
            var toDate = DateTime.ParseExact(request.ToDate, "yyyy-MM-dd", null);
            projectBriefsQueryable = projectBriefsQueryable.Where(x => x.CreatedAt <= toDate);
        }

        if(request.Keyword != null && request.Keyword.Trim().Length > 0)
        {
            projectBriefsQueryable = projectBriefsQueryable.Where(x => x.ProjectBriefNumber.Contains(request.Keyword.Trim(), StringComparison.OrdinalIgnoreCase) ||
                                                                  x.Description.Contains(request.Keyword.Trim(), StringComparison.OrdinalIgnoreCase) ||
                                                                  x.AboutBuyer.Contains(request.Keyword.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        var projectBriefsList = await _repository.ToListAsync<QuickHire.Domain.ProjectBriefs.ProjectBrief>(projectBriefsQueryable);
        projectBriefsList = projectBriefsList.OrderByDescending(x => x.CreatedAt).ToList();
        var projectBriefsIds = projectBriefsList.Select(x => x.Id).ToList();

        var suitableSellersQueryable = _repository.GetAllReadOnly<QuickHire.Domain.ProjectBriefs.SuitableSellerProjectBrief>().Where(x => projectBriefsIds.Contains(x.ProjectBriefId));
        var suitableSellersList = await _repository.ToListAsync(suitableSellersQueryable);

        var sellersReachedByBrief = suitableSellersList.GroupBy(x => x.ProjectBriefId).ToDictionary(x => x.Key, x => x.Count());

        return projectBriefsList.Select(x => new BuyerProjectBriefModel
        {
            Id = x.Id,
            Date = x.CreatedAt.ToString("dd MMM yyyy"),
            DocumentNumber = x.ProjectBriefNumber,
            SellersReached = sellersReachedByBrief.TryGetValue(x.Id, out var count) ? count : 0,
            TotalOffers = x.CustomOffers.Count(),
            Order = x.Status == ProjectBriefStatus.OrderPlaced,
            Status = x.Status.ToString()
        });*/

        return new List<BuyerProjectBriefModel>
        {
            new BuyerProjectBriefModel
            {
                Id = 1,
                Date = "01 Jan 2023",
                DocumentNumber = "PB-001",
                SellersReached = 5,
                TotalOffers = 3,
                Order = true,
                Status = ProjectBriefStatus.OrderPlaced.ToString()
            },
            new BuyerProjectBriefModel
            {
                Id = 2,
                Date = "15 Feb 2023",
                DocumentNumber = "PB-002",
                SellersReached = 10,
                TotalOffers = 7,
                Order = false,
                Status = ProjectBriefStatus.Delivered.ToString()
            }
        };
    }
}

