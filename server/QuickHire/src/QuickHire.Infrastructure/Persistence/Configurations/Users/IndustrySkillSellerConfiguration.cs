using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Users;

namespace QuickHire.Infrastructure.Persistence.Configurations.Users;

public class IndustrySkillSellerConfiguration : IEntityTypeConfiguration<IndustrySkillSeller>
{
    public void Configure(EntityTypeBuilder<IndustrySkillSeller> builder)
    {
        builder.HasKey(x => new { x.SellerId, x.IndustrySkillId });
    }
}
