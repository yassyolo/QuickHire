using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.ProjectBriefs;

namespace QuickHire.Infrastructure.Persistence.Configurations.ProjectBriefs;

internal class SuitableSellerProjectBriefConfiguration : IEntityTypeConfiguration<SuitableSellerProjectBrief>
{
    public void Configure(EntityTypeBuilder<SuitableSellerProjectBrief> builder)
    {
        builder.HasKey(x => new { x.SellerId, x.ProjectBriefId });

        builder.Property(x => x.SellerId).IsRequired();

        builder.HasOne(x => x.CustomOffer)
            .WithOne()
            .HasForeignKey<SuitableSellerProjectBrief>(x => x.CustomOfferId);

        builder.HasQueryFilter(x => !x.ProjectBrief.IsDeleted);
    }
}
