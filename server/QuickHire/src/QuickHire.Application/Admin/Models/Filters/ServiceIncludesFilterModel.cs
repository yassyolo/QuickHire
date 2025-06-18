namespace QuickHire.Application.Admin.Models.Filters;
public class ServiceIncludesFilterModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<FilterOptionModel> Options { get; set; } = new List<FilterOptionModel>();
}
