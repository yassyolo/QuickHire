using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Moderation.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Gig;


namespace QuickHire.Infrastructure.Persistence.Configurations.Gigs;

internal class GigConfiguration : IEntityTypeConfiguration<Gig>
{
    public void Configure(EntityTypeBuilder<Gig> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(TitleMaxLength);

        builder.Property(x => x.ImageUrls).IsRequired();

        builder.Property(x => x.SellerId).IsRequired();

        builder.Property(x => x.ModerationStatus)
            .HasConversion(x => x.ToString(),
            x => (ModerationStatus)Enum.Parse(typeof(ModerationStatus), x));

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Gig)
            .HasForeignKey(x => x.GigId);
    }
}
