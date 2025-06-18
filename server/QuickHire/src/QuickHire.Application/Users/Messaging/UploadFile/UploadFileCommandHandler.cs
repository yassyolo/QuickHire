using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Users.Messaging.UploadFile;

public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, string>
{
    private readonly ICloudinaryService _cloudinaryService;

    public UploadFileCommandHandler(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        if( request.File != null)
        {
            var uploadResult = _cloudinaryService.UploadFile(request.File);
            if (uploadResult != null)
            {
                return uploadResult;
            }
            else
            {
                throw new Exception("File upload failed.");
            }
        }

        return string.Empty;
    }
}
