using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponce>(ILogger<LoggingBehavior<TRequest, TResponce>> logger)
    : IPipelineBehavior<TRequest, TResponce>
    where TRequest : notnull , IRequest<TResponce>
    where TResponce : notnull
{
    public async Task<TResponce> Handle(TRequest request, RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={TRequestName} - Responce={TResponceName} - RequestData={RequestData}",
            typeof(TRequest).Name, typeof(TResponce).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var responce = await next();

        timer.Stop();
        var takenTime = timer.Elapsed;
        if (takenTime.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} taken {TakenTime}",
                typeof(TRequest).Name, takenTime.Seconds);
        }

        logger.LogInformation("[END] Handled {Request} with {Responce}",typeof(TRequest).Name, typeof(TResponce).Name);

        return responce;
    }
}
