using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.CustomOffers;

namespace QuickHire.Infrastructure.Persistence.Configurations.CustomOffers;

public class CustomOfferInclusivesConfiguration : IEntityTypeConfiguration<CustomOfferInclusives>
{
    public void Configure(EntityTypeBuilder<CustomOfferInclusives> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.CustomOffer)
            .WithMany()
            .HasForeignKey(x => x.CustomOfferId);

        builder.HasOne(x => x.PaymentPlanInclude)
            .WithMany()
            .HasForeignKey(x => x.PaymentPlanIncludeId);
    }
}
