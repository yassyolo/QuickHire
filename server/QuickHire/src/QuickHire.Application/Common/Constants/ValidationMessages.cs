namespace QuickHire.Application.Common.Constants;

internal static class ValidationMessages
{
    public const string Required = "{0} is required.";
    public const string GreaterThan = "{0} must be greater than {1}.";
    public const string StringLength = "{0} must be between {1} and {2} characters long.";
    public const string InvalidEmail = "A valid email address is required.";
    public const string InvalidPassword = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
}
