using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomItem;

namespace QuickHire.Infrastructure.Persistence.Configurations.Orders;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(CustomItemNumberMaxLength);

        builder.HasOne(x => x.SelectedPaymentPlan)
            .WithOne()
            .HasForeignKey<Order>(x => x.SelectedPaymentPlanId);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.ToString(),
            x => (OrderStatus)Enum.Parse(typeof(OrderStatus), x));
    }
}
