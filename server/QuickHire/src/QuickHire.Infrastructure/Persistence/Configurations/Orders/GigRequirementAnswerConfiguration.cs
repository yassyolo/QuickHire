using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Orders;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.GigRequirementAnswer;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Files;

namespace QuickHire.Infrastructure.Persistence.Configurations.Orders;

internal class GigRequirementAnswerConfiguration : IEntityTypeConfiguration<GigRequirementAnswer>
{
    public void Configure(EntityTypeBuilder<GigRequirementAnswer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Answer).IsRequired().HasMaxLength(GigRequirementAnswerMaxLength);

        builder.Property(x => x.AttachmentUrls).HasMaxLength(FileUrlMaxLength);

        builder.Property(x => x.BuyerId).IsRequired();

        builder.HasOne(x => x.GigRequirement)
            .WithOne()
            .HasForeignKey<GigRequirementAnswer>(x => x.GigRequirementId); 

        builder.HasOne(x => x.Order)
            .WithMany(x => x.GigRequirementAnswers)
            .HasForeignKey(x => x.OrderId);
    }
}
