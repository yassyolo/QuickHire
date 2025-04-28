using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.ProjectBriefs.Enums;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.CustomItem;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.ProjectBrief;


namespace QuickHire.Infrastructure.Persistence.Configurations.ProjectBriefs;

internal class ProjectBriefConfiguration : IEntityTypeConfiguration<ProjectBrief>
{
    public void Configure(EntityTypeBuilder<ProjectBrief> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProjectBriefNumber).IsRequired().HasMaxLength(CustomItemNumberMaxLength);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.AboutBuyer).IsRequired().HasMaxLength(AboutBuyerMaxLength);

        builder.Property(x => x.Budget)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.HasOne(x => x.MainCategory)
            .WithMany()
            .HasForeignKey(x => x.MainCategoryId);

        builder.HasOne(x => x.SubCategory)
            .WithMany()
            .HasForeignKey(x => x.SubCategoryId);

        builder.Property(x => x.BuyerId).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion(x => x.ToString(),
            x => (ProjectBriefStatus)Enum.Parse(typeof(ProjectBriefStatus), x));
    }
}
