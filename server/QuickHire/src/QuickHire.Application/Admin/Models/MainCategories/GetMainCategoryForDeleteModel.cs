namespace QuickHire.Application.Admin.Models.MainCategories;

public class GetMainCategoryForDeleteModel
{
    public int Id { get; set; }
    public IEnumerable<SubCategoriesInMainCategoryModel>? SubCategories { get; set; }
}
