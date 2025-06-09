using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Details;
using QuickHire.Domain.Shared.Exceptions;
using System.Security.Cryptography.X509Certificates;

namespace QuickHire.Application.Gigs.Details.GigDetails;

public class GetGigDetailsQueryHandler : IQueryHandler<GetGigDetailsQuery, GigDetailsModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetGigDetailsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public PaymentPlanModel[] PaymentPlans { get; set; } = Array.Empty<PaymentPlanModel>();
    public class PaymentPlanModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DeliveryTimeInDays { get; set; }
        public int Revisions { get; set; }
        public List<PaymentPlanIncludeModel> Inclusions { get; set; } = new();
    }
    public GigMetaDataModel[] GigMetaData { get; set; } = Array.Empty<GigMetaDataModel>();
    public int OrdersInQueue { get; set; }
    public int NumberOfLikes { get; set; }
    public bool Liked { get; set; }
    public int SellerId { get; set; }

    public async Task<GigDetailsModel> Handle(GetGigDetailsQuery request, CancellationToken cancellationToken)
    {
        /*var gigsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Gigs.Gig>().Where(x => x.Id == request.Id);
        gigsQueryable = _repository.GetAllIncluding<QuickHire.Domain.Gigs.Gig>(
                                  x => x.SubSubCategory.SubCategory.MainCategory,
                                  x => x.PaymentPlans,
                                  x => x.Metadata,
                                             x => x.Orders
);
        var gig = await _repository.FirstOrDefaultAsync(gigsQueryable);
        if (gig == null)
        {
            throw new NotFoundException(nameof(QuickHire.Domain.Gigs.Gig), request.Id);
        }

        var model = new GigDetailsModel
        {
            MainCategoryId = gig.SubSubCategory.SubCategory.MainCategory.Id,
            SubCategoryId = gig.SubSubCategory.SubCategory.Id,
            MainCategoryName = gig.SubSubCategory.SubCategory.MainCategory.Name,
            SubCategoryName = gig.SubSubCategory.SubCategory.Name,
            Title = gig.Title,
            Description = gig.Description,
            ImageUrls = gig.ImageUrls.ToArray()  
        };

        var paymentPlansQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Gigs.PaymentPlan>().Where(x => x.GigId == gig.Id);
        paymentPlansQueryable = _repository.GetAllIncluding<QuickHire.Domain.Gigs.PaymentPlan>(x => x.Inclusions);
        var paymentPlans = await _repository.ToListAsync(paymentPlansQueryable);
        model.PaymentPlans = paymentPlans.Select(x => new QuickHire.Application.Gigs.Models.Details.PaymentPlanModel
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Description = x.Description,
            DeliveryTimeInDays = x.DeliveryTimeInDays,
            Revisions = x.Revisions,
            Inclusions = x.Inclusions.Select(x => new QuickHire.Application.Gigs.Models.Details.PaymentPlanIncludeModel
            {
                Name = x.Name,
                Value = x.Value
            }).ToList()
        }).ToArray();


        var gigMetadataQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Gigs.GigMetadata>().Where(x => x.GigId == gig.Id);
        var gigMetadata = await _repository.ToListAsync(gigMetadataQueryable);
        var filterOptionIds = gigMetadata.Select(x => x.FilterOptionId).ToList();
        var filterOptionsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Categories.FilterOption>().Where(x => filterOptionIds.Contains(x.Id));
        filterOptionsQueryable = _repository.GetAllIncluding<QuickHire.Domain.Categories.FilterOption>(x => x.GigFilter);
        var filterOptions = await _repository.ToListAsync(filterOptionsQueryable);
        var groupedOptions = filterOptions
    .GroupBy(x => x.GigFilter) 
    .Select(x => new GigMetaDataModel
    {
        Title = x.Key.Title,
        Items = x.Select(x => x.Name).ToArray()
    }).ToArray();

        model.GigMetaData = groupedOptions;

        var favouritesQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.FavouriteGig>().Where(x => x.GigId == gig.Id);
        var favourites = await _repository.ToListAsync(favouritesQueryable);

        if (request.Preview)
        {
            var buyerId = await _userService.GetBuyerIdByUserIdAsync();
            model.OrdersInQueue = gig.Orders.Count();
            model.NumberOfLikes = favourites.Count();
            model.Liked = favourites.Any(x => x.BuyerId == buyerId);
            model.SellerId = gig.Seller.Id;
        }

        return model;*/

        return new GigDetailsModel
        {
            MainCategoryId = 1,
            SubCategoryId = 2,
            MainCategoryName = "Design",
            SubCategoryName = "Logo Design",
            Title = "I will create a professional logo for your brand",
            Description = "I offer high-quality custom logos tailored to your business needs.",
            ImageUrls = new[]
    {
        "https://example.com/image1.jpg",
        "https://example.com/image2.jpg"
    },
            PaymentPlans = new[]
    {
        new QuickHire.Application.Gigs.Models.Details.PaymentPlanModel
        {
            Id = 101,
            Name = "Basic",
            Price = 50,
            Description = "Basic logo with 2 revisions",
            DeliveryTimeInDays = 2,
            Revisions = 2,
            Inclusions = new List<QuickHire.Application.Gigs.Models.Details.PaymentPlanIncludeModel>
            {
                new () { Name = "High Resolution", Value = "true" },
                new () { Name = "Vector File", Value = "false" }
            }
        },
        new QuickHire.Application.Gigs.Models.Details.PaymentPlanModel
        {
            Id = 102,
            Name = "Standard",
            Price = 100,
            Description = "Standard logo package with 3 revisions",
            DeliveryTimeInDays = 3,
            Revisions = 3,
            Inclusions = new List<QuickHire.Application.Gigs.Models.Details.PaymentPlanIncludeModel>
            {
                new PaymentPlanIncludeModel { Name = "High Resolution", Value = "true" },
                new PaymentPlanIncludeModel { Name = "Vector File", Value = "true" },
                new PaymentPlanIncludeModel { Name = "Source File", Value = "false" }
            }
        },
        new QuickHire.Application.Gigs.Models.Details.PaymentPlanModel
        {
            Id = 103,
            Name = "Premium",
            Price = 200,
            Description = "Premium branding package with unlimited revisions",
            DeliveryTimeInDays = 5,
            Revisions = int.MaxValue, // Representing "unlimited"
            Inclusions = new List<PaymentPlanIncludeModel>
            {
                new PaymentPlanIncludeModel { Name = "High Resolution", Value = "true" },
                new PaymentPlanIncludeModel { Name = "Vector File", Value = "true" },
                new PaymentPlanIncludeModel { Name = "Source File", Value = "true" }
            }
        }
    },
            GigMetaData = new[]
    {
        new GigMetaDataModel
        {
            Title = "File Format",
            Items = new[] { "JPG", "PNG", "SVG" }
        },
        new GigMetaDataModel
        {
            Title = "Style",
            Items = new[] { "Minimalist", "Vintage", "3D" }
        }
    },
            OrdersInQueue = 3,
            NumberOfLikes = 42,
            Liked = true,
            SellerId = 1
        };
    }
}
