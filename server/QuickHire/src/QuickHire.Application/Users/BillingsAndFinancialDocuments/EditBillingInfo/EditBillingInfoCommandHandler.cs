using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.EditBillingInfo;

public class EditBillingInfoCommandHandler : ICommandHandler<EditBillingInfoCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public EditBillingInfoCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<Unit> Handle(EditBillingInfoCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetCurrentUserIdAsync();

        var existingBillingDetails = await _repository.GetByIdAsync<Domain.Users.BillingDetails, int>(request.Id);
        if (existingBillingDetails == null)
        {
            throw new NotFoundException(nameof(Domain.Users.BillingDetails), request.Id);
        }

        var address = await _repository.GetByIdAsync<Domain.Users.Address, int>(existingBillingDetails.AddressId);
        if (address == null)
        {
            throw new NotFoundException(nameof(Domain.Users.Address), existingBillingDetails.AddressId);
        }

        try
        {
            existingBillingDetails.FullName = request.FullName;
            existingBillingDetails.CompanyName = request.CompanyName;
            await _repository.UpdateAsync(existingBillingDetails);

            address.Street = request.Street;
            address.City = request.City;
            address.ZipCode = request.ZipCode;
            address.CountryId = request.CountryId;
            await _repository.UpdateAsync(address);
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while updating billing information.", ex.Message);
        }       

        return Unit.Value;
    }
}
