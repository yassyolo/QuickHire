using Microsoft.AspNetCore.Http;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface ICloudinaryService
{
    string UploadFile(IFormFile file);
}
