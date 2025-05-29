namespace QuickHire.Application.Admin.Models.SubCategories;

public class GetSubCategoryForDeleteModel
{
    public int Id { get; set; }
    public IEnumerable<SubSubCategoryForSubCategoryModel>? SubSubCategories { get; set; }
}
