using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.CustomOffers;
using QuickHire.Domain.Gigs;

namespace QuickHire.Application.CustomOffers.Seller.ChooseFromInclusives;

public class ChooseFromInclusivesQueryHandler : IQueryHandler<ChooseFromInclusivesQuery, List<InclusivesModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public ChooseFromInclusivesQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<List<InclusivesModel>> Handle(ChooseFromInclusivesQuery request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var gigsQueryable = _repository.GetAllReadOnly<Gig>().Where(x => x.SellerId == sellerId);
        var gigsList = await _repository.ToListAsync(gigsQueryable);
        var gigsIds = gigsList.Select(x => x.Id).ToList();

        var paymentPlans = _repository.GetAllIncluding<PaymentPlan>(x => x.Inclusions).Where(x => gigsIds.Contains(x.GigId));
        var paymentPlansList = await _repository.ToListAsync(paymentPlans);
        var distinctInclusives = paymentPlansList.SelectMany(x => x.Inclusions).GroupBy(x => x.Name)
         .Select(x => x.First())
         .Select(x => new InclusivesModel
         {
             Id = x.Id,
             Name = x.Name,
             Value = x.Value
         })
         .ToList();

        return distinctInclusives;
    }
}
