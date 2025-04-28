using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Categories.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.GigFilter;


namespace QuickHire.Infrastructure.Persistence.Configurations.Categories;

internal class GigFilterConfiguration : IEntityTypeConfiguration<GigFilter>
{
    public void Configure(EntityTypeBuilder<GigFilter> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(GigFilterTitleMaxLength);

        builder.Property(x => x.Type)
            .HasConversion(x => x.ToString(),
            x => (GigFilterType)Enum.Parse(typeof(GigFilterType), x));
    }
}
