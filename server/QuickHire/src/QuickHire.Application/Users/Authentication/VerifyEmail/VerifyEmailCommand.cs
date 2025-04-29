using MediatR;
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Users.Authentication.VerifyEmail;

internal record VerifyEmailCommand(VerifyEmailModel model) : IRequest<VerifyEmailResponseModel>;
