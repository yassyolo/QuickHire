using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Domain.Shared.Exceptions;
using static QuickHire.Application.Common.Constants.LoggingFormats;
using UnauthorizedAccessException = QuickHire.Domain.Shared.Exceptions.UnauthorizedAccessException;

namespace QuickHire.Api.Exceptions;

public class CustomExceptionHandling : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandling> _logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(ErrorExceptionFormat, nameof(httpContext.Request), nameof(exception), DateTime.Now);

        (string Details, string Title, int StatusCode) details = exception switch
        {
            InternalServerErrorException ex => (
            ex.Message,
            ex.GetType().Name,
            ex.StatusCode),

            NotFoundException ex => (
            ex.Message,
            ex.GetType().Name,
            ex.StatusCode),

            BadRequestException ex => (
            ex.Message,
            ex.GetType().Name,
            ex.StatusCode),

            ConflictException ex => (
            ex.Message,
            ex.GetType().Name,
            ex.StatusCode),

            UnauthorizedAccessException ex => (
            ex.Message,
            ex.GetType().Name,
            ex.StatusCode),

            _ => (
                exception.Message,
                nameof(InternalServerErrorException),
                httpContext.Response.StatusCode = 500)
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Details,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path,
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if(exception is ValidationException validationExc)
        {
            problemDetails.Extensions.Add("ValidationError", validationExc.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
