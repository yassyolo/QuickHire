using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Address;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Country)
            .WithOne()
            .HasForeignKey<Address>(x => x.CountryId);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(StreetMaxLength);

        builder.Property(x => x.City).IsRequired().HasMaxLength(CityMaxLength);

        builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(ZipCodeMaxLength);

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.IsBillingAddress).IsRequired();
    }
}
