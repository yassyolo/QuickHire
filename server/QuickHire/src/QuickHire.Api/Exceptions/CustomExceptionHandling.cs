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

    public CustomExceptionHandling(ILogger<CustomExceptionHandling> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(ErrorExceptionFormat, httpContext.Request.Path, exception.ToString(), DateTime.Now);

        if (exception is ValidationException validationExc)
        {
            var validationErrors = validationExc.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            var validationProblemDetails = new ValidationProblemDetails(validationErrors)
            {
                Title = "One or more validation failures occurred.",
                Status = StatusCodes.Status400BadRequest,
                Instance = httpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            validationProblemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(validationProblemDetails, cancellationToken: cancellationToken);
            return true; 
        }

        (string Details, string Title, int StatusCode) details = exception switch
        {
            InternalServerErrorException ex => (ex.Message, ex.GetType().Name, ex.StatusCode),
            NotFoundException ex => (ex.Message, ex.GetType().Name, ex.StatusCode),
            BadRequestException ex => (ex.Message, ex.GetType().Name, ex.StatusCode),
            ConflictException ex => (ex.Message, ex.GetType().Name, ex.StatusCode),
            UnauthorizedAccessException ex => (ex.Message, ex.GetType().Name, ex.StatusCode),
            _ => (
                exception.Message,
                nameof(InternalServerErrorException),
                StatusCodes.Status500InternalServerError
            )
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Details,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path,
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        httpContext.Response.StatusCode = details.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return true;
    }



}
