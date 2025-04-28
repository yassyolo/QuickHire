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
            .HasForeignKey(x => x.GigId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.UserId).IsRequired();
        
        builder.Property(x => x.ViewedAt).IsRequired();
    }
}
