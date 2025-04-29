using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(x => x.UserId).IsRequired();

        builder.HasMany(x => x.Portfolios)
            .WithOne(x => x.Seller)
            .HasForeignKey(x => x.SellerId)
            .IsRequired();

        builder.HasMany(x => x.Certifications)
            .WithOne(x => x.Seller)
            .HasForeignKey(x => x.SellerId)
            .IsRequired();

        builder.HasMany(x => x.Educations)
           .WithOne(x => x.Seller)
           .HasForeignKey(x => x.SellerId)
           .IsRequired();

        builder.HasMany(x => x.Skills)
           .WithOne(x => x.Seller)
           .HasForeignKey(x => x.SellerId)
           .IsRequired();

        builder.HasMany(x => x.Gigs)
           .WithOne(x => x.Seller)
           .HasForeignKey(x => x.SellerId);

        builder.HasMany(x => x.Conversations)
           .WithOne(x => x.Seller)
           .HasForeignKey(x => x.SellerId);

        builder.HasMany(x => x.SoldOrders)
           .WithOne(x => x.Seller)
           .HasForeignKey(x => x.SellerId);

        builder.HasMany(x => x.CustomOffers)
           .WithOne(x => x.Seller)
           .HasForeignKey(x => x.SellerId);
    }
}
