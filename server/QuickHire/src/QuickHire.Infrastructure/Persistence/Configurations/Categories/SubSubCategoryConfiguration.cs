using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Categories;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Category;

namespace QuickHire.Infrastructure.Persistence.Configurations.Categories;

internal class SubSubCategoryConfiguration : IEntityTypeConfiguration<SubSubCategory>
{
    public void Configure(EntityTypeBuilder<SubSubCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.HasMany(x => x.Gigs)
            .WithOne(x => x.SubSubCategory)
            .HasForeignKey(x => x.SubSubCategoryId);

        builder.HasMany(x => x.GigFilters)
            .WithOne(x => x.SubSubCategory)
            .HasForeignKey(x => x.SubSubCategoryId);
    }
}
