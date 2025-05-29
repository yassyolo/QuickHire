using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Education;


namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Institution).IsRequired().HasMaxLength(InstitutionMaxLength);

        builder.Property(x => x.GraduationYear).IsRequired();

        builder.Property(x => x.Major).IsRequired().HasMaxLength(InstitutionMaxLength);

        builder.Property(x => x.Degree)
            .IsRequired()
            .HasConversion(x => x.ToString(),
            x => (EducationDegree)Enum.Parse(typeof(EducationDegree), x));
    }
}
