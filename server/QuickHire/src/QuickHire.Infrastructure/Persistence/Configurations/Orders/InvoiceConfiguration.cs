using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Orders;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomItem;


namespace QuickHire.Infrastructure.Persistence.Configurations.Orders;

internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(CustomItemNumberMaxLength);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.TotalAmount).IsRequired().HasPrecision(8, 2);

        builder.Property(x => x.Tax).IsRequired().HasPrecision(8, 2);

        builder.Property(x => x.Subtotal).IsRequired().HasPrecision(8, 2);

        builder.Property(x => x.ServiceFee).IsRequired().HasPrecision(8, 2);

        builder.HasOne(x => x.Order)
            .WithOne(x => x.Invoice)
            .HasForeignKey<Invoice>(x => x.OrderId);
    }
}
