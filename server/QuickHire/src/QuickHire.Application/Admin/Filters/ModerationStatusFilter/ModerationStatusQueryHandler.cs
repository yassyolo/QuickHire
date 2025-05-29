using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Domain.Moderation.Enums;

namespace QuickHire.Application.Admin.Filters.ModerationStatusFilter;

public class ModerationStatusQueryHandler : IQueryHandler<ModerationStatusQuery, FilterItemModel[]>
{
    public async Task<FilterItemModel[]> Handle(ModerationStatusQuery request, CancellationToken cancellationToken)
    {
        return new FilterItemModel[]
        {
            new ()
            {
                Id = (int) ModerationStatus.Active,
                Name = ModerationStatus.Active.ToString()
            },
            new ()
            {
                Id = (int) ModerationStatus.Deactivated,
                Name = ModerationStatus.Deactivated.ToString()
            },
            new ()
        {
            Id = (int) ModerationStatus.PendingReview,
            Name = SplitPascalCase(ModerationStatus.PendingReview.ToString())
        },
        new ()
        {
            Id = (int) ModerationStatus.PendingVerification,
            Name = SplitPascalCase(ModerationStatus.PendingVerification.ToString())
        }
        };
    }

    private string SplitPascalCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1"); 
    }
}
