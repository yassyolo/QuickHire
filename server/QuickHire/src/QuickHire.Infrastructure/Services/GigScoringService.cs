using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.ProjectBriefs;
using QuickHire.Domain.Gigs;
using QuickHire.Infrastructure.Persistence.EFHelpers;
using QuickHire.Infrastructure.Persistence.Identity;

namespace QuickHire.Infrastructure.Services;

public class GigScoringService : IGigScoringService
{ 
    private readonly IRepository _repository;
    public GigScoringService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GigScoreModel>> GetTopScoringGigsAsync(string aboutBuyer, string description, int subSubCategoryId, decimal budget, int deliveryDays)
    {
        var gigsQueryable = _repository.GetAllReadOnly<Gig>().Where(x => x.ModerationStatus != Domain.Moderation.Enums.ModerationStatus.Deactivated && x.SubSubCategoryId == subSubCategoryId);
        gigsQueryable = _repository.GetAllIncluding<Gig>(x => x.PaymentPlans, x => x.Tags, x => x.Seller);

        var gigList = await _repository.ToListAsync<Gig>(gigsQueryable);

        var result = new List<GigScoreModel>();

        foreach (var gig in gigList)
        {
            double score = 0;

            var inputKeywords = (aboutBuyer + " " + description).Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Distinct().ToList();
            var inputSoundexes = inputKeywords.Select(SqlFunctions.Soundex).Where(x => x != null).ToHashSet();

            var gigText = $"{gig.Title} {gig.Description}";
            var gigWords = gigText.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Distinct().ToList();

            int matchCount = 0;

            foreach (var word in gigWords)
            {
                var soundex = SqlFunctions.Soundex(word);
                if (soundex != null && inputSoundexes.Contains(soundex))
                {
                    matchCount++;
                }
            }

            score += Math.Min(20, matchCount * 2);

            foreach (var tag in gig.Tags)
            {
                if (aboutBuyer.Contains(tag.Name, StringComparison.OrdinalIgnoreCase) || description.Contains(tag.Name, StringComparison.OrdinalIgnoreCase))
                {
                    score += 5;
                }
            }            

            if (gig.Clicks > 10)
            {
                score += Math.Min(15, gig.Clicks * 0.1);
            }

            if (gig.PaymentPlans.Any(x => x.Price >= budget * 0.9m && x.Price <= budget * 1.1m))
            {
                score += 10;
            }

            if (gig.PaymentPlans.Any(x => x.DeliveryTimeInDays <= deliveryDays))
            {
                score += 10;
            }

            var seller = gig.Seller;
            var user = await _repository.GetByIdAsync<ApplicationUser, string>(seller.UserId);

            var sellerWords = user.Description?.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Distinct();

                if (sellerWords != null)
                {
                    int sellerMatchCount = sellerWords.Count(word =>
                    {
                        var soundex = SqlFunctions.Soundex(word);
                        return soundex != null && inputSoundexes.Contains(soundex);
                    });

                    score += Math.Min(10, sellerMatchCount * 1); 
                }

                if (seller.Clicks > 0)
                {
                    score += Math.Min(10, seller.Clicks * 0.1); 
                }

            var matchingIndustrySkills = await _repository.ToListAsync(_repository.GetAllReadOnly<Domain.Users.IndustrySkillSeller>().Where(x => x.SellerId == seller.Id && x.IndustrySkillId == subSubCategoryId));

            if (matchingIndustrySkills.Any()) score += 10;

            result.Add(new GigScoreModel
            {
                GigId = gig.Id,
                Score = score,
                Gig = gig,
                SellerId = seller.Id,
            });
        }

        return result.OrderByDescending(x => x.Score).Take(20).ToList();
    }
}
