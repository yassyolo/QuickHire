namespace QuickHire.Application.Common.Constants;

internal static class LoggingFormats
{
    public static string PerformanceWarningFormat =>
        "[PERFORMANCE] Request {RequestName} took {ElapsedMilliseconds}ms - RequestData: {RequestData}";

    public static string ValidationErrorFormat =>
        "[VALIDATION] Request {RequestName} - RequestData: {RequestData} - Errors: {Errors}";

    public static string InformationStartFormat =>
        "[START] Request {RequestName} - Response {ResponseName}  - RequestData: {RequestData}";

    public static string InformationEndFormat =>
        "[END] Request {RequestName} - Response {ResponseName} - RequestData: {RequestData} - ResponseData: {ResponseData}";
}
