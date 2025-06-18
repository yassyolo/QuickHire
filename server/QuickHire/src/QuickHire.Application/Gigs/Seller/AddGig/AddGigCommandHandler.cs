using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.AddGig;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Categories.Enums;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Shared.Constants;
using Stripe;
using System.Numerics;
using System.Text.Json;

namespace QuickHire.Application.Gigs.Seller.AddGig;

public class AddGigCommandHandler : ICommandHandler<AddGigCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly ICloudinaryService _cloudinaryService;

    public AddGigCommandHandler(IRepository repository, IUserService userService, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _userService = userService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Unit> Handle(AddGigCommand request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var gig = new Domain.Gigs.Gig
        {
            Title = request.Title,
            Description = request.Description,
            SubSubCategoryId = request.SubSubCategoryId,
            SellerId = sellerId,
            ModerationStatus = Domain.Moderation.Enums.ModerationStatus.Active,
            CreatedAt = DateTime.UtcNow,
            Clicks = 0,
        };

        await _repository.AddAsync(gig);
        await _repository.SaveChangesAsync();
        if (request.GalleryImages != null && request.GalleryImages.Any())
        {
            foreach (var image in request.GalleryImages)
            {
                var uploadResult = _cloudinaryService.UploadFile(image);
                gig.ImageUrls.Add(uploadResult);
            }
        }

        var tags = request.Tags.Split(',').Select(tag => tag.Trim()).ToList();
        foreach (var tag in tags)
        {
            var newTag = new Tag
            {
                Name = tag,
                GigId = gig.Id
            };
            await _repository.AddAsync(newTag);
        }

        var requirements = JsonSerializer.Deserialize<List<RequirementModel>>(request.Requirements);
        if (requirements != null && requirements.Any())
        {
            foreach (var requirement in requirements)
            {
                var newRequirement = new GigRequirement
                {
                    Question = requirement.Question,
                    GigId = gig.Id
                };
                await _repository.AddAsync(newRequirement);
            }
        }

        var faqs = JsonSerializer.Deserialize<List<FAQModel>>(request.Faqs);
        if (faqs != null && faqs.Any())
        {
            foreach (var faq in faqs)
            {
                var newFaq = new FAQ
                {
                    Question = faq.Question,
                    Answer = faq.Answer,
                    GigId = gig.Id
                };
                await _repository.AddAsync(newFaq);
            }
        }

        var gigMetada = JsonSerializer.Deserialize<List<SelectedOptionModel>>(request.Metadata);
        if (gigMetada != null && gigMetada.Any())
        {
            foreach (var metadata in gigMetada)
            {
                var newMetadata = new GigMetadata
                {
                    FilterOptionId = metadata.OptionId,
                    GigId = gig.Id
                };
                await _repository.AddAsync(newMetadata);
            }
        }
        var deliveryTimeFilterQueryable = _repository.GetAllIncluding<GigFilter>(x => x.Options)
   .Where(f => f.Type == GigFilterType.DeliveryTime);
        var deliveryTimeFilter = await _repository.FirstOrDefaultAsync(deliveryTimeFilterQueryable);

        var priceRangeFilterQueryable = _repository.GetAllIncluding<GigFilter>(x => x.Options)
            .Where(f => f.Type == GigFilterType.PriceRange);
        var priceRangeFilter = await _repository.FirstOrDefaultAsync(priceRangeFilterQueryable);

       
        var paymentPlans = JsonSerializer.Deserialize<List<PaymentPlanModel>>(request.Plans);
        if (paymentPlans != null && paymentPlans.Any())
        {
            foreach (var plan in paymentPlans)
            {
                var newPlan = new PaymentPlan
                {
                    Name = plan.Name,
                    Description = plan.Description,
                    Price = plan.Price,
                    DeliveryTimeInDays = plan.DeliveryTimeInDays,
                    Revisions = plan.Revisions,
                    GigId = gig.Id
                };
                await _repository.AddAsync(newPlan);
                await _repository.SaveChangesAsync();

                if (plan.Inclusions != null && plan.Inclusions.Any())
                {
                    foreach (var include in plan.Inclusions)
                    {
                        var newInclude = new PaymentPlanInclude
                        {
                            Name = include.Name,
                            Value = include.Value,
                            PaymentPlanId = newPlan.Id
                        };
                        await _repository.AddAsync(newInclude);
                    }
                }

                var mappedName = MapDeliveryTimeToFilterOptionName(plan.DeliveryTimeInDays);

                var deliveryOptionQueryable = _repository.GetAllReadOnly<FilterOption>()
                    .Where(o => o.Name == mappedName && o.GigFilter.Type == GigFilterType.DeliveryTime);
                var deliveryOption = await _repository.FirstOrDefaultAsync(deliveryOptionQueryable);

                if (deliveryOption != null)
                {
                    var deliveryMetadata = new GigMetadata
                    {
                        FilterOptionId = deliveryOption.Id,
                        GigId = gig.Id
                    };
                    await _repository.AddAsync(deliveryMetadata);
                }

                var mappedPriceName = MapPriceToFilterOptionName(plan.Price);
                var priceOptionQueryable = _repository.GetAllReadOnly<FilterOption>()
                    .Where(o => o.Name == mappedPriceName && o.GigFilter.Type == GigFilterType.PriceRange);

                var priceOption = await _repository.FirstOrDefaultAsync(priceOptionQueryable);
                if (priceOption != null)
                {
                    var priceMetadata = new GigMetadata
                    {
                        FilterOptionId = priceOption.Id,
                        GigId = gig.Id
                    };
                    await _repository.AddAsync(priceMetadata);
                }
            }
        }


        await _repository.UpdateAsync(gig);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }

    private string MapDeliveryTimeToFilterOptionName(int deliveryDays)
    {
        return deliveryDays switch
        {
            <= 1 => FilterOptionsDescriptions.DeliveryTime.Express,
            <= 3 => FilterOptionsDescriptions.DeliveryTime.UpTo3Days,
            <= 7 => FilterOptionsDescriptions.DeliveryTime.UpTo7Days,
            _ => FilterOptionsDescriptions.DeliveryTime.Anytime,
        };
    }

    private string MapPriceToFilterOptionName(decimal price)
    {
        return price switch
        {
            <= 50 => FilterOptionsDescriptions.PriceRange.Under,
            <= 200 => FilterOptionsDescriptions.PriceRange.MidRange,
            _ => FilterOptionsDescriptions.PriceRange.HighEnd
        };
    }


}
