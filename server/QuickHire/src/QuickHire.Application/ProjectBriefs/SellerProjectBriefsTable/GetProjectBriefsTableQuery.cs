using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.ProjectBriefs;

namespace QuickHire.Application.ProjectBriefs.SellerProjectBriefsTable;

public record GetProjectBriefsTableQuery() : IQuery<List<SellerProjectBriefTableModel>>;

