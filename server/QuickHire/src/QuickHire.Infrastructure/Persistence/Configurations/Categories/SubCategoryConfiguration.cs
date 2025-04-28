using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Categories;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Files;

namespace QuickHire.Infrastructure.Persistence.Configurations.Categories;

internal class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(FileUrlMaxLength);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.HasMany(x => x.SubSubCategories)
            .WithOne(x => x.SubCategory)
            .HasForeignKey(x => x.SubCategoryId);
    }
}
