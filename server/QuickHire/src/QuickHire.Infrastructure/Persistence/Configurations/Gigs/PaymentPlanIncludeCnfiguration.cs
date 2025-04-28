using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Gigs;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.PaymentPlanInclude;

namespace QuickHire.Infrastructure.Persistence.Configurations.Gigs;

internal class PaymentPlanIncludeCnfiguration : IEntityTypeConfiguration<PaymentPlanInclude>
{
    public void Configure(EntityTypeBuilder<PaymentPlanInclude> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.Property(x => x.Value).IsRequired().HasMaxLength(ValueMaxLength);

        builder.HasOne(x => x.PaymentPlan)
            .WithMany(x => x.Inclusions)
            .HasForeignKey(x => x.PaymentPlanId);
    }
}
