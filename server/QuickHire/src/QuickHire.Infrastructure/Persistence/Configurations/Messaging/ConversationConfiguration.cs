using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Messaging;

namespace QuickHire.Infrastructure.Persistence.Configurations.Messaging;

internal class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.LastMessageAt).IsRequired();

        builder.Property(x => x.IsStarredByBuyer).IsRequired();

        builder.Property(x => x.IsStarredBySeller).IsRequired();

        builder.HasOne(x => x.Order)
            .WithOne(x => x.Conversation)
            .HasForeignKey<Conversation>(x => x.OrderId);

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId);

        builder.Property(x => x.ParticipantAId).IsRequired();
        builder.Property(x => x.ParticipantAMode).IsRequired();

        builder.Property(x => x.ParticipantBId).IsRequired();
        builder.Property(x => x.ParticipantBMode).IsRequired();
    }
}
