using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickHire.Domain.Categories;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Moderation;
using QuickHire.Domain.Orders;
using QuickHire.Domain.ProjectBriefs;
using QuickHire.Domain.Shared.Contracts;
using QuickHire.Domain.Users;
using QuickHire.Infrastructure.Persistence.EFHelpers;
using QuickHire.Infrastructure.Persistence.Identity;
using System.Linq.Expressions;
using System.Reflection.Emit;
using ApplicationUser = QuickHire.Infrastructure.Persistence.Identity.ApplicationUser;

namespace QuickHire.Infrastructure.Persistence;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    //public ApplicationDbContext()
    //{
    //}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        ApplySoftDeleteQueryFilter(builder);
        ApplyDefaultDeleteBehavior(builder);

        builder.HasDbFunction(typeof(SqlFunctions).GetMethod(nameof(SqlFunctions.Soundex))).HasName("SOUNDEX");
    }

    private void ApplyDefaultDeleteBehavior(ModelBuilder builder)
    {
        foreach (var fk in builder.Model
                           .GetEntityTypes()
                           .SelectMany(x => x.GetForeignKeys()))
        {
            if(fk.DeleteBehavior == DeleteBehavior.Cascade)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }

    private void ApplySoftDeleteQueryFilter(ModelBuilder builder)
    {
        foreach(var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                var param = Expression.Parameter(entityType.ClrType, "x");
                var isDeletedFalse = Expression.Equal(
                                     Expression.Property(param, nameof(ISoftDeletable.IsDeleted)),
                                     Expression.Constant(false));
                var lambda = Expression.Lambda(isDeletedFalse, param);
                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BillingDetails> BillingDetails { get; set; }
    public DbSet<BrowsingHistory> BrowsingHistories { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<FavouriteGig> FavouriteGigs { get; set; }
    public DbSet<FavouriteGigsList> FavouriteGigsLists { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<UserLanguage> UserLanguages { get; set;}
    public DbSet<FAQ> FAQs { get; set;}
    public DbSet<FilterOption> FilterOptions { get; set; }
    public DbSet<GigFilter> GigFilters { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<SubSubCategory> SubSubCategories { get; set; }
    public DbSet<CustomOffer> CustomOffers { get; set; }
    public DbSet<Gig> Gigs { get; set; }
    public DbSet<GigMetadata> GigMetadatas { get; set; }
    public DbSet<GigRequirement> GigRequirements { get; set; }
    public DbSet<PaymentPlan> PaymentPlans { get; set; }
    public DbSet<PaymentPlanInclude> PaymentPlanIncludes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<DeactivatedRecord> DeactivatedRecords { get; set; }
    public DbSet<ReportedItem> ReportedItems { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<GigRequirementAnswer> GigRequirementAnswers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDeliveryDate> OrderDeliveryDates { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Revision> Revisions { get; set; }
    public DbSet<ProjectBrief> ProjectBriefs { get; set; }
    public DbSet<SuitableSellerProjectBrief> SuitableSellerProjectBriefs { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<IndustrySkillSeller> IndustrySkillSellers { get; set; }
}
        

