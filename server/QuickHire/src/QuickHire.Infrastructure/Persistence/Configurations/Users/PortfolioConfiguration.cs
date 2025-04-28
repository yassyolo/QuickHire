using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Portfolio;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Files;



namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(TitleMaxLength);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.VideoUrl).HasMaxLength(FileUrlMaxLength);

        builder.Property(x => x.UserId).IsRequired();
    }
}
