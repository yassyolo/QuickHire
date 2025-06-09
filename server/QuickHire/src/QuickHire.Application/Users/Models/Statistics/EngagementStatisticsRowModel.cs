namespace QuickHire.Application.Users.Models.Statistics;

public class EngagementStatisticsRowModel
{
    public string Date { get; set; } = null!;
    public int ProfileViews { get; set; }
    public int GigClicks { get; set; }
    public int MessagesStarted { get; set; }
    public int GigSaves { get; set; }
    public int BriefsReceived { get; set; }
}
