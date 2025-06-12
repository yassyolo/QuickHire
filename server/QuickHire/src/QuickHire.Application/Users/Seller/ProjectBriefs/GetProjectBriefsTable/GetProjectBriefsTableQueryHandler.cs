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
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var suitableProjectBriefsQueryable = _repository.GetAllIncluding<Domain.ProjectBriefs.SuitableSellerProjectBrief>(x => x.ProjectBrief).Where(x => x.SellerId == sellerId);
        var suitableProjectBriefs = await _repository.ToListAsync<Domain.ProjectBriefs.SuitableSellerProjectBrief>(suitableProjectBriefsQueryable);

        var model =  await Task.WhenAll(suitableProjectBriefs.Select(async x => new SellerProjectBriefTableModel
        {
            Id = x.ProjectBrief.Id,
            BuyerUsername = await _userService.GetUsernameByBuyerIdAsync(x.ProjectBrief.BuyerId),
            Description = x.ProjectBrief.Description,
            DeliveryTimeInDays = x.ProjectBrief.DeliveryTimeInDays.ToString(),
            Budget = x.ProjectBrief.Budget
        }).ToList()
        );

        return model.ToList();
    }
}

