using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Orders.Models.Form;
using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;
using QuickHire.Domain.Gigs;

namespace QuickHire.Application.Orders.PlaceOrder.OrderForm;

public class GetOrderFormQueryHandler : IQueryHandler<GetOrderFormQuery, OrderFormModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetOrderFormQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<OrderFormModel> Handle(GetOrderFormQuery request, CancellationToken cancellationToken)
    {
        var gigRequirementsQueryable = _repository.GetAllReadOnly<GigRequirement>().Where(x => x.GigId == request.GigId);
        var gigRequirements = await _repository.ToListAsync(gigRequirementsQueryable);
        var userId = _userService.GetCurrentUserIdAsync();

        var billingInfoQueryable = _repository.GetAllIncluding<Domain.Users.BillingDetails>(x => x.Address.Country).Where(x => x.UserId == userId);
        var billingInfo = await _repository.FirstOrDefaultAsync(billingInfoQueryable);
        return new OrderFormModel
        {
            BillingDetails = billingInfo != null ? new GetBillingInfoModel
            {
                Id = billingInfo.Id!,
                FullName = billingInfo.FullName!,
                CompanyName = billingInfo.CompanyName,
                Street = billingInfo.Address.Street,
                City = billingInfo.Address.City,
                ZipCode = billingInfo.Address.ZipCode,
                Country = billingInfo.Address.Country.Name,
                CountryId = billingInfo.Address.CountryId
            } : null,
            GigRequirements = gigRequirements.Select(x => new GigRequirementModel
            {
                Id = x.Id,
                Question = x.Question,
            }).ToList()
        };
    }
}

