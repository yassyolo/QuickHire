using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Categories;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FilterOption;


namespace QuickHire.Infrastructure.Persistence.Configurations.Categories;

internal class FilterOptionConfiguration : IEntityTypeConfiguration<FilterOption>
{
    public void Configure(EntityTypeBuilder<FilterOption> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.HasOne(x => x.GigFilter)
            .WithMany(x => x.Options)
            .HasForeignKey(x => x.GigFilterId);
    }
}
