using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickHire.Domain.Orders;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength.Review;

namespace QuickHire.Infrastructure.Persistence.Configurations.Orders;

internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Comment).IsRequired().HasMaxLength(CommentMaxLength);

        builder.Property(x => x.Rating).IsRequired().HasMaxLength(RatingStarsMaxLength);

        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.OrderId);
    }
}
