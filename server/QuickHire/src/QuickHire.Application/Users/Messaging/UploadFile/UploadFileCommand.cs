using Microsoft.AspNetCore.Http;
using QuickHire.Application.Common.Interfaces.Abstractions;
using System.Windows.Input;

namespace QuickHire.Application.Users.Messaging.UploadFile;

public record UploadFileCommand(IFormFile File) : ICommand<string>;
