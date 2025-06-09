using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.EditBillingInfo;

public record EditBillingInfoCommand(int Id, string FullName, string CompanyName, int CountryId, string Street, string City, string ZipCode) : ICommand<Unit>;

