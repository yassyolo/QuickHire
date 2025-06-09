using QuickHire.Application.Admin.Models.SubSubCategories;

namespace QuickHire.Application.Admin.Models.SubSubCategories;

public class SubSubCategoryDetailsModel 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Clicks { get; set; }
    public string CreatedOn { get; set; } = string.Empty;
    public string SubCategoryName { get; set; } = string.Empty;
    public List<GigFilterModel> GigFilters { get; set; } = new List<GigFilterModel>();
}
