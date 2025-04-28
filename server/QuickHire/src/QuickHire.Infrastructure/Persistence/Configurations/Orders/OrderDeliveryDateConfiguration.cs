using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Orders;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.OrderDeliveryDate;


namespace QuickHire.Infrastructure.Persistence.Configurations.Orders;

internal class OrderDeliveryDateConfiguration : IEntityTypeConfiguration<OrderDeliveryDate>
{
    public void Configure(EntityTypeBuilder<OrderDeliveryDate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DeliveryDate).IsRequired();

        builder.Property(x => x.IsChanged).IsRequired();

        builder.Property(x => x.ChangeDateReason).HasMaxLength(ChangeDateReason);

        builder.HasOne(x => x.Order)
            .WithOne()
            .HasForeignKey<OrderDeliveryDate>(x => x.OrderId);
    }
}
