namespace QuickHire.Infrastructure.CloudStorage;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Infrastructure.Options;
using static QuickHire.Infrastructure.CloudStorage.Constants.FolderOrganization;
using static QuickHire.Infrastructure.CloudStorage.Constants;
using QuickHire.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinaryOptions _cloudinaryOptions;

    public CloudinaryService(IOptions<CloudinaryOptions> options)
    {
        _cloudinaryOptions = options.Value;
        var account = new Account(_cloudinaryOptions.CloudName, _cloudinaryOptions.ApiKey, _cloudinaryOptions.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public string UploadFile(IFormFile file)
    {
        if(file == null || file.Length == 0)
        {
            throw new BadRequestException("File is null or empty", $"File {nameof(file)} not existing.");
        }

        if (file.Length > 10 * 1024 * 1024)
        {
            throw new BadRequestException("File size exceeded", $"File size {file.Length} exceeds the limit of 10MB.");
        }

        var extension = Path.GetExtension(file.FileName).ToLower();

        string mimeType = extension switch
        {
            SupportedExtensions.Jpg or SupportedExtensions.Jpeg => SupportedMimeTypes.Jpg,
            SupportedExtensions.Png => SupportedMimeTypes.Png,
            SupportedExtensions.Pdf => SupportedMimeTypes.Pdf,
            SupportedExtensions.Mp4 => SupportedMimeTypes.Mp4,
            _ => throw new BadRequestException("Unsupported file type", $"File type {extension} is not supported.")
        };

        if (mimeType == "image/jpeg" || mimeType == "image/png")
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Transformation = new Transformation().Width(500).Height(500).Crop("limit").Quality("auto"),
                Format = extension.Substring(1),
                Overwrite = true,
                PublicId = Image + Guid.NewGuid().ToString(),
            };

            var uploadUri = _cloudinary.Upload(uploadParams);
            return uploadUri?.SecureUri?.ToString();
        }
        else if (mimeType == "application/pdf")
        {
            var rawUploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),   
                PublicId = Document + Guid.NewGuid().ToString(),
            };

            var uploadResult = _cloudinary.Upload(rawUploadParams);
            return uploadResult?.SecureUrl?.ToString();
        }
        else if (mimeType == "video/mp4")
        {
            var videoUploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Transformation = new Transformation().Width(500).Height(500).Crop("limit").Quality("auto"),
                Format = extension.Substring(1),
                Overwrite = true,
                PublicId = FolderOrganization.Video + Guid.NewGuid().ToString(),
            };

            var uploadResult = _cloudinary.Upload(videoUploadParams);
            return uploadResult?.SecureUrl?.ToString();
        }

        return string.Empty;
    }
}
