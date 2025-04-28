using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.FavouriteGigsList;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

internal class FavouriteGigListConfiguration : IEntityTypeConfiguration<FavouriteGigsList>
{
    public void Configure(EntityTypeBuilder<FavouriteGigsList> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(NameMaxLength);

        builder.Property(x => x.Description).HasMaxLength(DescriptionMaxLength);

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();
    }
}
