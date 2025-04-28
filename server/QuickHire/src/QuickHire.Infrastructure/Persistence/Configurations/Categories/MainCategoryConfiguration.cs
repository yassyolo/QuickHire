using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Categories;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.MainCategory;

namespace QuickHire.Infrastructure.Persistence.Configurations.Categories;

internal class MainCategoryConfiguration : IEntityTypeConfiguration<MainCategory>
{
    public void Configure(EntityTypeBuilder<MainCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.HasMany(x => x.SubCategories)
            .WithOne(x => x.MainCategory)
            .HasForeignKey(x => x.MainCategoryId);
    }
}
