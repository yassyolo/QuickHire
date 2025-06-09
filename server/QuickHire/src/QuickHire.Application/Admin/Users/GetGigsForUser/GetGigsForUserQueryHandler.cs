using Mapster;
using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Moderation.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickHire.Application.Admin.Users.GetGigsForUser;

public class GetGigsForUserQueryHandler : IQueryHandler<GetGigsForUserQuery, PaginatedResultModel<SearchGigsForAdminModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetGigsForUserQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<PaginatedResultModel<SearchGigsForAdminModel>> Handle(GetGigsForUserQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var gigsQueryable = _repository.GetAllReadOnly<Gig>().Where(x => x.SellerId == sellerId);
        gigsQueryable = _repository.GetAllIncluding<Gig>(x => x.Orders, x => x.SubSubCategory);        

        var totalCount = gigsQueryable.Count();
        IEnumerable<Gig> gigsList;

        if (totalCount <= request.ItemsPerPage)
        {
            gigsList = await _repository.ToListAsync(gigsQueryable.OrderBy(x => x.Id));
        }
        else
        {
            var pagedQuery = gigsQueryable
                .Skip((request.CurrentPage - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage);

            gigsList = await _repository.ToListAsync(pagedQuery);
        }
        var gigsForAdminModels = gigsList
            .Select(x => new SearchGigsForAdminModel
            {
                Id = x.Id,
                CreatedOn = x.CreatedAt.ToString("yyyy-MM-dd"),
                Service = x.Title,
                Orders = x.Orders.Count(),
                Revenue = x.Orders.Sum(x => x.TotalPrice),
                AvgReview = x.Orders.SelectMany(o => o.Reviews).Any() ? x.Orders.SelectMany(o => o.Reviews).Average(r => r.Rating) : 0,
                Clicks = x.Clicks,
                SubSubCategoryName = x.SubSubCategory.Name,
                Status = x.ModerationStatus.ToString()
            });

        return new PaginatedResultModel<SearchGigsForAdminModel>()
         {
             Data = gigsForAdminModels,
             TotalPages = (int)Math.Ceiling(totalCount / (double)request.ItemsPerPage)
         };*/

        return new PaginatedResultModel<SearchGigsForAdminModel>
        {
            Data = new List<SearchGigsForAdminModel>
            {
                new SearchGigsForAdminModel
                {
                    Id = 1,
                    CreatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                    Service = "Sample Gig",
                    Orders = 5,
                    Revenue = 1000,
                    AvgReview = 4.5,
                    Clicks = 150,
                    SubSubCategoryName = "Web Development",
                    Status = ModerationStatus.Active.ToString()
                },
                new SearchGigsForAdminModel
                {
                    Id = 2,
                    CreatedOn = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd"),
                    Service = "Another Gig",
                    Orders = 3,
                    Revenue = 500,
                    AvgReview = 4.0,
                    Clicks = 80,
                    SubSubCategoryName = "Graphic Design",
                    Status = ModerationStatus.PendingReview.ToString()
                }
            },
            TotalPages = 1
        };
    }
}

