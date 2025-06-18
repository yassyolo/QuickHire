using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Moderation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ModerationItem;


namespace QuickHire.Infrastructure.Persistence.Configurations.Moderation;

internal class ReportedItemConfiguration : IEntityTypeConfiguration<ReportedItem>
{
    public void Configure(EntityTypeBuilder<ReportedItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReportedById).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Reason).IsRequired().HasMaxLength(ReasonMaxLength);
    }
}
