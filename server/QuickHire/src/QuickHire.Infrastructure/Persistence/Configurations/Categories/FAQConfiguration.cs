using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FAQ;
using QuickHire.Domain.Categories;

namespace QuickHire.Infrastructure.Persistence.Configurations.Categories;

internal class FAQConfiguration : IEntityTypeConfiguration<FAQ>
{
    public void Configure(EntityTypeBuilder<FAQ> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Question).IsRequired().HasMaxLength(QuestionMaxLength);

        builder.Property(x => x.Answer).IsRequired().HasMaxLength(AnswerMaxLength);

        builder.HasOne(x => x.MainCategory)
            .WithMany(x => x.FAQs)
            .HasForeignKey(x => x.MainCategoryId);

        builder.HasOne(x => x.Gig)
            .WithMany(x => x.FAQs)
            .HasForeignKey(x => x.GigId);   
    }
}
