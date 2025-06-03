using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(x => x.UserId).IsRequired();

        builder.HasMany(x => x.FavouriteGigsLists)
           .WithOne(x => x.Buyer)
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.FavouriteGigs)
            .WithOne(x => x.Buyer)
              .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.Notifications)
            .WithOne(x => x.Buyer)
              .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.Invoices)
           .WithOne(x => x.Buyer)
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.Conversations)
           .WithOne(x => x.Buyer)
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.PlacedOrders)
           .WithOne(x => x.Buyer)
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.CustomOffers)
           .WithOne(x => x.Buyer)
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.ProjectBriefs)
           .WithOne(x => x.Buyer)
           .HasForeignKey(x => x.BuyerId);
    }
}
