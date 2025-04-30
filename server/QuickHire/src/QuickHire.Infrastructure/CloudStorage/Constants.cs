namespace QuickHire.Infrastructure.CloudStorage;

internal static class Constants
{
    internal static class SupportedMimeTypes
    {
        public const string Pdf = "application/pdf";
        public const string Jpg = "image/jpeg";
        public const string Jpeg = "image/jpeg";
        public const string Png = "image/png";
        public const string Mp4 = "video/mp4";
    }

    internal static class SupportedExtensions
    {
        public const string Pdf = ".pdf";
        public const string Jpg = ".jpg";
        public const string Jpeg = ".jpeg";
        public const string Png = ".png";
        public const string Mp4 = ".mp4";
    }

    internal static class SupportedFileSize
    {
        public const int MaxSizeInBytes = 10 * 1024 * 1024; 
    }

    internal static class FolderOrganization
    {
        public const string Image = "images/";
        public const string Document = "documents/";
        public const string Video = "videos/";
    }
}
