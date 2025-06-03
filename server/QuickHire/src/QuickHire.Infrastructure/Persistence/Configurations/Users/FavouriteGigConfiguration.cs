using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class FavouriteGigConfiguration : IEntityTypeConfiguration<FavouriteGig>
{
    public void Configure(EntityTypeBuilder<FavouriteGig> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Buyer)
            .WithMany()
            .HasForeignKey(x => x.BuyerId)
            .IsRequired();

        builder.Property(x => x.AddedAt).IsRequired();

        builder.HasOne(x => x.Gig)
            .WithMany()
            .HasForeignKey(x => x.GigId);

        builder.HasOne(x => x.FavouriteGigsList)
            .WithMany(x => x.FavouriteGigs)
            .HasForeignKey(x => x.FavouriteGigsListId);

        builder.HasQueryFilter(x => !x.Gig.IsDeleted);
    }
}
