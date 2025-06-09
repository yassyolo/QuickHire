using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.ProjectBriefs;

namespace QuickHire.Application.Users.Seller.ProjectBriefs.GetProjectBriefsTable;

public class GetProjectBriefsTableQueryHandler : IQueryHandler<GetProjectBriefsTableQuery, List<SellerProjectBriefTableModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetProjectBriefsTableQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<List<SellerProjectBriefTableModel>> Handle(GetProjectBriefsTableQuery request, CancellationToken cancellationToken)
    {
        /*var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var suitableProjectBriefsQueryable = _repository.GetAllReadOnly<Domain.ProjectBriefs.SuitableSellerProjectBrief>().Where(x => x.SellerId == sellerId);
        suitableProjectBriefsQueryable = _repository.GetAllIncluding<Domain.ProjectBriefs.SuitableSellerProjectBrief>(x => x.ProjectBrief);
        var suitableProjectBriefs = await _repository.ToListAsync<Domain.ProjectBriefs.SuitableSellerProjectBrief>(suitableProjectBriefsQueryable);

        var model =  await Task.WhenAll(suitableProjectBriefs.Select(async x => new SellerProjectBriefTableModel
        {
            Id = x.ProjectBrief.Id,
            BuyerUsername = await _userService.GetUsernameByUserIdAsync(x.ProjectBrief.BuyerId),
            Description = x.ProjectBrief.Description,
            DeliveryTimeInDays = x.ProjectBrief.DeliveryTimeInDays.ToString(),
            Budget = x.ProjectBrief.Budget
        }).ToList()
        );

        return model.ToList();*/

        return new List<SellerProjectBriefTableModel>
        {
            new SellerProjectBriefTableModel
            {
                Id = 1,
                BuyerUsername = "buyer1",
                Description = "Project brief description 1",
                DeliveryTimeInDays = "7",
                Budget = 1000.00m
            },
            new SellerProjectBriefTableModel
            {
                Id = 2,
                BuyerUsername = "buyer2",
                Description = "Project brief description 2",
                DeliveryTimeInDays = "14",
                Budget = 2000.00m
            }
        };
    }
}

