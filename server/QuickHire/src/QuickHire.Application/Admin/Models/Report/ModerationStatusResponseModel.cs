namespace QuickHire.Application.Admin.Models.Report;

public class ModerationStatusResponseModel
{
    public string Status { get; set; } = string.Empty;
    public List<ReportTableModel> ModerationStatus { get; set; } = new List<ReportTableModel>();
}
