using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Gigs;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.PaymentPlan;


namespace QuickHire.Infrastructure.Persistence.Configurations.Gigs;

internal class PaymentPlanConfiguration : IEntityTypeConfiguration<PaymentPlan>
{
    public void Configure(EntityTypeBuilder<PaymentPlan> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.DeliveryTimeInDays).IsRequired();

        builder.Property(x => x.Revisions).IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(8, 2);

        builder.HasOne(x => x.Gig)
            .WithMany(x => x.PaymentPlans)
            .HasForeignKey(x => x.GigId);
    }
}
