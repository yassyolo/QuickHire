using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Dashboard;

namespace QuickHire.Application.Users.Seller.Dashboard.GetSellerDashboard;

public class GetSellerDashboardQueryHandler : IQueryHandler<GetSellerDashboardQuery, GetSellerDashboardModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetSellerDashboardQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<GetSellerDashboardModel> Handle(GetSellerDashboardQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var sellerDashboardInfo = await _userService.GetSellerDashboardInfoAsync(sellerId);

        var orderQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Order>(x => x.Reviews).Where(x => x.SellerId == sellerId);
        var orderList = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(orderQueryable);

        return new GetSellerDashboardModel()
        {
            Name = sellerDashboardInfo.name,
            ProfilePictureUrl = sellerDashboardInfo.profilePictureUrl,
            Username = sellerDashboardInfo.username,
            ThisMonth = DateTime.Now.ToString("MMMM", new System.Globalization.CultureInfo("en-US")),
            EarningsThisMonth = orderList.Where(x => x.CreatedAt.Month == DateTime.Now.Month && x.CreatedAt.Year == DateTime.Now.Year).Sum(x => x.TotalPrice).ToString(),
            TotalOrders = orderList.Count(),
            AverageRating = orderList.SelectMany(x => x.Reviews).Select(x => x.Rating).DefaultIfEmpty()  .Average(),
            TotalReviews = orderList.Sum(x => x.Reviews.Count()),
        };
    }
}

