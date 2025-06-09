using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.ProjectBriefs;

namespace QuickHire.Application.Users.Seller.ProjectBriefs.GetProjectBriefsTable;

public record GetProjectBriefsTableQuery() : IQuery<List<SellerProjectBriefTableModel>>;

