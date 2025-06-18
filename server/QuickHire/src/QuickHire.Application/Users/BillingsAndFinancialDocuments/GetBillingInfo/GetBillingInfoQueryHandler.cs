using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.GetBillingInfo;

public class GetBillingInfoQueryHandler : IQueryHandler<GetBillingInfoQuery, GetBillingInfoModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetBillingInfoQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<GetBillingInfoModel> Handle(GetBillingInfoQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetCurrentUserIdAsync();

        var billingInfoQueryable = _repository.GetAllIncluding<Domain.Users.BillingDetails>(x => x.Address.Country).Where(x => x.UserId == userId);
        var billingInfo = await _repository.FirstOrDefaultAsync<Domain.Users.BillingDetails>(billingInfoQueryable);

        return new GetBillingInfoModel
        {
            Id = billingInfo.Id,
            FullName = billingInfo.FullName,
            CompanyName = billingInfo.CompanyName!,
            Street = billingInfo.Address.Street,
            City = billingInfo.Address.City,
            ZipCode = billingInfo.Address.ZipCode,
            Country = billingInfo.Address.Country.Name,
            CountryId = billingInfo.Address.CountryId
        };
    }
}
