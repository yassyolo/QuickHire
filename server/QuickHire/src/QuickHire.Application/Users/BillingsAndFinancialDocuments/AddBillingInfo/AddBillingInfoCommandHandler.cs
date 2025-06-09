using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.AddBillingInfo;

public class AddBillingInfoCommandHandler : ICommandHandler<AddBillingInfoCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public AddBillingInfoCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(AddBillingInfoCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetCurrentUserIdAsync();
        var address = new QuickHire.Domain.Users.Address
        {
            UserId = userId,
            Street = request.Street,
            City = request.City,
            ZipCode = request.ZipCode,
            IsBillingAddress = true,
            CountryId = request.CountryId
        };

        await _repository.AddAsync(address);
        await _repository.SaveChangesAsync();

        var billingDetails = new BillingDetails
        {
            CompanyName = request.CompanyName,
            AddressId = address.Id,
            FullName = request.FullName,
        };

        await _repository.AddAsync(billingDetails);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

