using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Skill;


namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);
    }
}
