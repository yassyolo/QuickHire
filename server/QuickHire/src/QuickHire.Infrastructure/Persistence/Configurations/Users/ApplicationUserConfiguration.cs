using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Infrastructure.Persistence.Identity;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ApplicationUser;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Files;


namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.JoinedAt).IsRequired();

        builder.Property(x => x.UserName).IsRequired().HasMaxLength(UsernameMaxLength);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(FullNameMaxLength);

        builder.Property(x => x.Description)
            .HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.ProfileImageUrl)
            .HasMaxLength(FileUrlMaxLength);

        builder.HasMany(x => x.Notifications)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Languages)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.FAQs)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.ModerationStatus)
            .IsRequired()
            .HasConversion(x => x.ToString(),
            x => (ModerationStatus)Enum.Parse(typeof(ModerationStatus), x));
    }
}
