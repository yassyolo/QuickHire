using Mapster;
using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Moderation.Enums;

namespace QuickHire.Application.Admin.Gigs.SearchGigsForAdmin;

public class SearchGigsForAdminQueryHandler : IQueryHandler<SearchGigsForAdminQuery, PaginatedResultModel<SearchGigsForAdminModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public SearchGigsForAdminQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
   
    public async Task<PaginatedResultModel<SearchGigsForAdminModel>> Handle(SearchGigsForAdminQuery request, CancellationToken cancellationToken)
    {
        var gigsQueryable = _repository.GetAllReadOnly<Gig>();
        gigsQueryable = _repository.GetAllIncluding<Gig>(x => x.Orders, x => x.SubSubCategory);

        if (request.ModerationStatusId != null)
        {
            var parsedStatus = (ModerationStatus)request.ModerationStatusId.Value;
            gigsQueryable = gigsQueryable.Where(x => x.ModerationStatus == parsedStatus);
        }

        if (request.SubCategoryId != null)
        {
            gigsQueryable = gigsQueryable.Where(x => x.SubSubCategory.SubCategoryId == request.SubCategoryId);
        }

        if (request.SubSubCategoryId != null)
        {
            gigsQueryable = gigsQueryable.Where(x => x.SubSubCategoryId == request.SubSubCategoryId);
        }

        if (request.Id != null)
        {
            gigsQueryable = gigsQueryable.Where(x => x.Id == request.Id);
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            gigsQueryable = gigsQueryable.Where(x => x.Title.ToLower().Contains(request.Keyword.ToLower()) || x.Description.ToLower().Contains(request.Keyword.ToLower()));              
        };

        var totalCount = gigsQueryable.Count();

        IEnumerable<Gig> gigsList;

        if (totalCount <= request.ItemsPerPage)
        {
            gigsList = await _repository.ToListAsync(gigsQueryable.OrderBy(x => x.Id));
        }
        else
        {
            var pagedQuery = gigsQueryable.Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage);
            gigsList = await _repository.ToListAsync(pagedQuery);
        }

        var gigsForAdminModels = gigsList
            .Select(x => new SearchGigsForAdminModel
            {
                Id = x.Id,
                CreatedOn = x.CreatedAt.ToString("dd MMM, yyyy"),
                Service = x.Title,  
                Orders = x.Orders.Where(x => x.Status != Domain.Orders.Enums.OrderStatus.PendingPayment || x.Status != Domain.Orders.Enums.OrderStatus.Failed).Count(),
                Revenue = x.Orders.Where(x => x.Status != Domain.Orders.Enums.OrderStatus.PendingPayment || x.Status != Domain.Orders.Enums.OrderStatus.Failed).Sum(x => x.TotalPrice),
                AvgReview = x.Orders.Where(x => x.Status != Domain.Orders.Enums.OrderStatus.PendingPayment || x.Status != Domain.Orders.Enums.OrderStatus.Failed).SelectMany(x => x.Reviews).Any() ? x.Orders.SelectMany(x => x.Reviews).Average(x => x.Rating) : 0,
                Clicks = x.Clicks,
                SubSubCategoryName = x.SubSubCategory.Name,
                Status = x.ModerationStatus.ToString()
            });
           
        return new PaginatedResultModel<SearchGigsForAdminModel>()
        {
            Data = gigsForAdminModels,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.ItemsPerPage)
        };     
    }
}
