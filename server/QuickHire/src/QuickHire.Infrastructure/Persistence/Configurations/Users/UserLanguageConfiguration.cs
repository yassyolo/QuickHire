using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class UserLanguageConfiguration : IEntityTypeConfiguration<UserLanguage>
{
    public void Configure(EntityTypeBuilder<UserLanguage> builder)
    {
        builder.HasKey(x => new { x.UserId, x.LanguageId });

        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(x => x.Language)
            .WithMany()
            .HasForeignKey(x => x.LanguageId);

        builder.Property(x => x.Proficiency)
            .HasConversion(x => x.ToString(),
            x => (ProficiencyLevel)Enum.Parse(typeof(ProficiencyLevel), x));
    }
}
