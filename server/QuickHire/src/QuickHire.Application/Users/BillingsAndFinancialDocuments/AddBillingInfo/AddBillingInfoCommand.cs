using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.AddBillingInfo;

public record AddBillingInfoCommand(string FullName, string CompanyName, int CountryId, string Street, string City, string ZipCode) : ICommand<GetBillingInfoModel>;

