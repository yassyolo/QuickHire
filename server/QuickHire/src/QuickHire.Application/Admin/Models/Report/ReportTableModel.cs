namespace QuickHire.Application.Admin.Models.Report;
public class ReportTableModel
{
    public int Id { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public string CreatedOn { get; set; } = string.Empty;
}
