using QuickHire.Application.Users.Models.ProjectBriefs;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface IGigScoringService
{
    Task<List<GigScoreModel>> GetTopScoringGigsAsync(string title, string description, int subSubCategoryId, decimal budget, int deliveryDays);
}
