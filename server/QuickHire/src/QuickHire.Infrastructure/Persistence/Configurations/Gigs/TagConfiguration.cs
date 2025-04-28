using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Gigs;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Tag;


namespace QuickHire.Infrastructure.Persistence.Configurations.Gigs;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.HasOne(x => x.Gig)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.GigId);
    }
}
