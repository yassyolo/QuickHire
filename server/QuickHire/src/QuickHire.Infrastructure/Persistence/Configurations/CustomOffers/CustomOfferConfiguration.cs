using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.CustomOffers.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomOffer;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomItem;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.RejectedItems;

namespace QuickHire.Infrastructure.Persistence.Configurations.CustomOffers;

internal class CustomOfferConfiguration : IEntityTypeConfiguration<CustomOffer>
{
    public void Configure(EntityTypeBuilder<CustomOffer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomOfferNumber).IsRequired().HasMaxLength(CustomItemNumberMaxLength);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.Revisions).IsRequired().HasMaxLength(RevisionMaxCount);

        builder.Property(x => x.DeliveryTimeInDays).IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.RejectionReason).HasMaxLength(RejectionReasonMaxLength);

        builder.Property(x => x.WithdrawalReason).HasMaxLength(RejectionReasonMaxLength);

        builder.HasOne(x => x.Gig)
            .WithMany(x => x.CustomOffers)
            .HasForeignKey(x => x.GigId);

        builder.HasOne(x => x.Order)
            .WithOne(x => x.CustomOffer)
            .HasForeignKey<CustomOffer>(x => x.OrderId);

        builder.HasOne(x => x.ProjectBrief)
            .WithMany(x => x.CustomOffers)
            .HasForeignKey(x => x.ProjectBriefId);

        builder.Property(x => x.Status)
            .HasConversion(x => x.ToString(),
            x => (CustomOfferStatus)Enum.Parse(typeof(CustomOfferStatus), x));
    }
}
