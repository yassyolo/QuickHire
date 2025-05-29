using MediatR;
using FluentValidation;
using QuickHire.Application.Common.Interfaces.Abstractions;
using Microsoft.Extensions.Logging;
using static QuickHire.Application.Common.Constants.LoggingFormats;

namespace QuickHire.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Validating {typeof(TRequest).Name}");
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll
            (_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var failiures = validationResults.Where(x => x.Errors.Any()).SelectMany(x => x.Errors).ToList();

        if (failiures.Any())
        {
            _logger.LogError(ValidationErrorFormat, typeof(TRequest).Name, request, failiures);
            throw new ValidationException(failiures);
        }

        return await next();
    }
}
