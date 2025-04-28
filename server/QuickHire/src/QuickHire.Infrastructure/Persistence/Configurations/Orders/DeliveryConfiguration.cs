using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Orders;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Delivery;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Files;

namespace QuickHire.Infrastructure.Persistence.Configurations.Orders; 

internal class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.SourceFileUrl).HasMaxLength(FileUrlMaxLength);

        builder.Property(x => x.AttachmentUrls).IsRequired().HasMaxLength(FileUrlMaxLength);

        builder.HasOne(x => x.Order)
            .WithOne(x => x.Delivery)
            .HasForeignKey<Delivery>(x => x.OrderId);
    }
}
