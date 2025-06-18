using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Moderation;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ModerationItem;


namespace QuickHire.Infrastructure.Persistence.Configurations.Moderation;

internal class DeactivatedRecordConfiguration : IEntityTypeConfiguration<DeactivatedRecord>
{
    public void Configure(EntityTypeBuilder<DeactivatedRecord> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Reason).IsRequired().HasMaxLength(ReasonMaxLength);

        builder.HasOne(x => x.Gig)
            .WithOne()
            .HasForeignKey<DeactivatedRecord>(x => x.GigId);
    }
}
