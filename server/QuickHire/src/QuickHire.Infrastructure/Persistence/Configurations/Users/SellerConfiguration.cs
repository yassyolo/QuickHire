using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(b => b.UserId);

        builder.Property(x => x.UserId).IsRequired();

        builder.HasMany(x => x.Portfolios)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Certifications)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Educations)
           .WithOne()
           .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Skills)
           .WithOne()
           .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Gigs)
           .WithOne()
           .HasForeignKey(x => x.SellerId);

        builder.HasMany(x => x.Conversations)
           .WithOne()
           .HasForeignKey(x => x.BuyerId);

        builder.HasMany(x => x.SoldOrders)
           .WithOne()
           .HasForeignKey(x => x.SellerId);

        builder.HasMany(x => x.CustomOffers)
           .WithOne()
           .HasForeignKey(x => x.SellerId);
    }
}
