using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class BrowsingHistoryConfiguration : IEntityTypeConfiguration<BrowsingHistory>
{
    public void Configure(EntityTypeBuilder<BrowsingHistory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Gig)
            .WithMany()
            .HasForeignKey(x => x.GigId);

        builder.HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey(x => x.SellerId);

        builder.HasOne(x => x.Buyer)
            .WithMany(x => x.BrowsingHistories)
            .HasForeignKey(x => x.BuyerId);

        builder.HasQueryFilter(x => !x.Gig.IsDeleted);
        
        builder.Property(x => x.ViewedAt).IsRequired();
    }
}
