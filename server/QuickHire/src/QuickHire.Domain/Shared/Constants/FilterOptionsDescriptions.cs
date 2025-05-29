namespace QuickHire.Domain.Shared.Constants;

public static class FilterOptionsDescriptions
{
    public static class DeliveryTime
    {
        public const string Express = "Express24H";
        public const string UpTo3Days = "Up to 3 days";
        public const string UpTo7Days = "Up to 7 days";
        public const string Anytime = "Anytime";
    }

    public static class PriceRange
    {
        public const string Under = "Under $50";
        public const string MidRange = "$50 - $200";
        public const string HighEnd = "$200 and above";
    }
}
