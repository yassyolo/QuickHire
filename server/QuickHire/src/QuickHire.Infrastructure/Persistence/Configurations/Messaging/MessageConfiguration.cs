using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Messaging;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Message;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Files;

namespace QuickHire.Infrastructure.Persistence.Configurations.Messaging;

internal class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SenderId).IsRequired();

        builder.Property(x => x.ReceiverId).IsRequired();

        builder.Property(x => x.Text).IsRequired().HasMaxLength(TextMaxLength);

        builder.Property(x => x.SentAt).IsRequired();

        builder.Property(x => x.IsRead).IsRequired();

        builder.Property(x => x.AttachmentUrl).HasMaxLength(FileUrlMaxLength);
    }
}
