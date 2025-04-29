using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.BillingDetails;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class BillingDetailsConfiguration : IEntityTypeConfiguration<BillingDetails>
{
    public void Configure(EntityTypeBuilder<BillingDetails> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).IsRequired().HasMaxLength(FullNameMaxLength);

        builder.HasOne(x => x.Address)
            .WithOne()
            .HasForeignKey<BillingDetails>(x => x.AddressId);

        builder.HasQueryFilter(x => !x.Address.IsDeleted);

        builder.Property(x => x.CompanyName).HasMaxLength(CompanyNameMaxLength);
    }
}
