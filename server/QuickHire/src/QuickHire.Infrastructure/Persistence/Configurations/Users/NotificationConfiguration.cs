using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Notification;


namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(TitleMaxLength);

        builder.Property(x => x.Message).IsRequired().HasMaxLength(MessageMaxLength);

        builder.Property(x => x.IsRead).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.NotificationType)
            .IsRequired()
            .HasConversion(x => x.ToString(),
            x => (NotificationType)Enum.Parse(typeof(NotificationType), x));
    }
}
