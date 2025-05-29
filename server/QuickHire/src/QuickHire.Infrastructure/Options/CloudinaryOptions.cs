﻿namespace QuickHire.Infrastructure.Options;

public class CloudinaryOptions
{
    public const string CloudinaryOptionsKey = "CloudinaryOptions";
    public string CloudName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
}
