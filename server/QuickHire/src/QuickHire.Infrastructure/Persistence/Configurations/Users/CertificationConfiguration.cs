using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Certification;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.Property(x => x.Issuer).IsRequired().HasMaxLength(IssuerMaxLength);

        builder.Property(x => x.IssuedAt).IsRequired();

        builder.Property(x => x.UserId).IsRequired();
    }
}
