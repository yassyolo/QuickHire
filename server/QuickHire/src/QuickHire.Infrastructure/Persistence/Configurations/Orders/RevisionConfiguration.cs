using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Delivery;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.RejectedItems;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Files;

namespace QuickHire.Infrastructure.Persistence.Configurations.Orders;

internal class RevisionConfiguration : IEntityTypeConfiguration<Revision>
{
    public void Configure(EntityTypeBuilder<Revision> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Revisions)
            .HasForeignKey(x => x.OrderId);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.ToString(),
            x => (RevisionStatus)Enum.Parse(typeof(RevisionStatus), x));

        builder.Property(x => x.RejectionReason).HasMaxLength(RejectionReasonMaxLength);

        builder.Property(x => x.SourceFileUrl).HasMaxLength(FileUrlMaxLength);
    }
}
