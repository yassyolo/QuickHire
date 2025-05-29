namespace QuickHire.Application.Admin.Models.Shared;

public class PaginatedResultModel<T> where T : class
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public int TotalPages { get; set; }
}
