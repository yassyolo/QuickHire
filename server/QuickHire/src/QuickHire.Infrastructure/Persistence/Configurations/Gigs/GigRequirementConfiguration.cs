using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Gigs;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.GigRequirement;


namespace QuickHire.Infrastructure.Persistence.Configurations.Gigs;

internal class GigRequirementConfiguration : IEntityTypeConfiguration<GigRequirement>
{
    public void Configure(EntityTypeBuilder<GigRequirement> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Question).IsRequired().HasMaxLength(QuestionMaxLength);

        builder.HasOne(x => x.Gig)
            .WithMany(x => x.Requirements)
            .HasForeignKey(x => x.GigId);
    }
}
