namespace QuickHire.Application.Admin.Models.SubSubCategories;

public class GigFilterModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<FilterOptionModel> Items { get; set; } = new List<FilterOptionModel>();
}
