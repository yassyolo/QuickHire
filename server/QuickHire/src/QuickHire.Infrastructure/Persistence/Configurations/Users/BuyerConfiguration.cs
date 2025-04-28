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

        builder.HasOne(x => x.BillingDetails)
            .WithOne()
            .HasForeignKey<Buyer>(x => x.BillingDetailsId);

        builder.HasMany(x => x.BrowsingHistory)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.FavouriteGigsLists)
           .WithOne()
           .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Invoices)
           .WithOne()
           .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Conversations)
           .WithOne()
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.PlacedOrders)
           .WithOne()
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.CustomOffers)
           .WithOne()
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.ProjectBriefs)
           .WithOne()
           .HasForeignKey(x => x.BuyerId);
    }
}
