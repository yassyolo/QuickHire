using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.NotAuthenticated;

namespace QuickHire.Application.Users.Authentication.NotAuthenticatedPage;

public record NotAuthenticatedPageQuery() : IQuery<NotAuthenticatedPageModel>;
