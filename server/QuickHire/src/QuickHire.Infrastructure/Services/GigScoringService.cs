using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.ProjectBriefs;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Users;

namespace QuickHire.Infrastructure.Services;

public class GigScoringService : IGigScoringService
{
    private readonly IRepository _repository;
    public GigScoringService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GigScoreModel>> GetTopScoringGigsAsync(int buyerId, string aboutBuyer, string description, int subSubCategoryId, decimal budget, int deliveryDays)
    {
        var inputKeywords = (aboutBuyer + " " + description).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLowerInvariant()).Distinct().ToList();

        var gigsWithScoresQuery = _repository.GetAllIncluding<Gig>(x => x.PaymentPlans, x => x.Tags, x => x.Seller.SoldOrders)
            .Where(x => x.ModerationStatus != Domain.Moderation.Enums.ModerationStatus.Deactivated && x.SellerId != buyerId && x.SubSubCategoryId == subSubCategoryId)
            .Select(x => new
            {
                Gig = x,
                Seller = x.Seller,
                TitleLower = x.Title.ToLower(),
                DescriptionLower = x.Description.ToLower(),
                ScoreClicks = x.Clicks > 10 ? Math.Min(15, x.Clicks * 0.1) : 0,
                ScoreBudget = x.PaymentPlans.Any(p => p.Price >= budget * 0.9m && p.Price <= budget * 1.1m) ? 10 : 0,
                ScoreDelivery = x.PaymentPlans.Any(p => p.DeliveryTimeInDays <= deliveryDays) ? 10 : 0,
                Tags = x.Tags.Select(t => t.Name),
                SellerClicks = x.Seller.Clicks
            });

        var gigsData = await gigsWithScoresQuery.ToListAsync();

        var results = new List<GigScoreModel>();

        foreach (var item in gigsData)
        {
            double score = item.ScoreClicks + item.ScoreBudget + item.ScoreDelivery;

            foreach (var keyword in inputKeywords)
            {
                if (item.TitleLower.Contains(keyword))
                {
                    score += 3; 
                }
            }

            foreach (var keyword in inputKeywords)
            {
                if (item.DescriptionLower.Contains(keyword))
                {
                    score += 1; 
                }
            }

            foreach (var tagName in item.Tags)
            {
                if ((aboutBuyer + " " + description).IndexOf(tagName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    score += 5;
                }
            }

            var allReviews = item.Seller?.SoldOrders.SelectMany(o => o.Reviews).ToList();
            if (allReviews != null && allReviews.Count > 0)
            {
                var avgRating = allReviews.Average(r => r.Rating);
                if (avgRating > 4.5)
                {
                    score += 10;
                }
            }

            if (item.Seller.Skills.Any())
            {
                score += item.Seller.Skills.Count(skill => inputKeywords.Any(keyword => skill.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))) * 2;
            }

            if (item.SellerClicks > 0)
            {
                score += Math.Min(10, item.SellerClicks * 0.1);
            }

            results.Add(new GigScoreModel
            {
                GigId = item.Gig.Id,
                Gig = item.Gig,
                SellerId = item.Seller.Id,
                Score = score
            });
        }

        var distinctGigsBySeller = results.GroupBy(x => x.SellerId).Select(x => x.OrderByDescending(x => x.Score).First()).OrderByDescending(x => x.Score).Take(20).ToList();

        return distinctGigsBySeller;
    }
}
