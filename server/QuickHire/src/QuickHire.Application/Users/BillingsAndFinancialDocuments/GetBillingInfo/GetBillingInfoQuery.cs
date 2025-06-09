using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.GetBillingInfo;

public record GetBillingInfoQuery() : IQuery<GetBillingInfoModel>;
