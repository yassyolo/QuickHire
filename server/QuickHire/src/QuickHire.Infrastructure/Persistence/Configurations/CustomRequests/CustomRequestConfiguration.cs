using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.CustomRequests;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomOffer;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomItem;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.RejectedItems;

namespace QuickHire.Infrastructure.Persistence.Configurations.CustomRequests;

internal class CustomRequestConfiguration : IEntityTypeConfiguration<CustomRequest>
{
    public void Configure(EntityTypeBuilder<CustomRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomRequestNumber).IsRequired().HasMaxLength(CustomItemNumberMaxLength);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.BuyerId).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.RejectedReason).HasMaxLength(RejectionReasonMaxLength);

        builder.Property(x => x.DeliveryTimeInDays).IsRequired();

        builder.HasOne(x => x.Gig)
            .WithMany(x => x.CustomRequests)
            .HasForeignKey(x => x.GigId);

        builder.Property(x => x.Budget)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.HasOne(x => x.Message)
            .WithOne(x => x.CustomRequest)
            .HasForeignKey<CustomRequest>(x => x.MessageId);
    }
}
