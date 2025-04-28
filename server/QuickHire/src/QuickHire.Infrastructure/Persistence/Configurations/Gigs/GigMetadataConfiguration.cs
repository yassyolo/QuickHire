using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Gigs;

namespace QuickHire.Infrastructure.Persistence.Configurations.Gigs;

internal class GigMetadataConfiguration : IEntityTypeConfiguration<GigMetadata>
{
    public void Configure(EntityTypeBuilder<GigMetadata> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Gig)
            .WithMany(x => x.Metadata)
            .HasForeignKey(x => x.GigId);

        builder.HasOne(x => x.FilterOption)
            .WithOne()
            .HasForeignKey<GigMetadata>(x => x.GigId);
    }
}
