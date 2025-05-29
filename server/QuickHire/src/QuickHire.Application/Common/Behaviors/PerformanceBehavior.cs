using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static QuickHire.Application.Common.Constants.LoggingFormats;

namespace QuickHire.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {       
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var response = await next();

        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.Elapsed;

        if(stopwatch.ElapsedMilliseconds > 300)
        {
            _logger.LogWarning(PerformanceWarningFormat, typeof(TRequest).Name, typeof(TResponse).Name, elapsedMilliseconds, request);
        }

        return response;
    }
}
