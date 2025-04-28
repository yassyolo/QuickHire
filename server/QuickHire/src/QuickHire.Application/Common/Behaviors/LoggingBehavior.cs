using MediatR;
using Microsoft.Extensions.Logging;
using static QuickHire.Application.Common.Constants.LoggingFormats;

namespace QuickHire.Application.Common.Behaviors;

internal class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation(InformationStartFormat, typeof(TRequest).Name, typeof(TResponse).Name, request);

        var response = await next();

        logger.LogInformation(InformationEndFormat, typeof(TRequest).Name, typeof(TResponse).Name, request, response);

        return response;
    }
}
