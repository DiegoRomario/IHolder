using IHolder.Domain.Common;
using IHolder.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IHolder.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware(RequestDelegate next, ILogger<EventualConsistencyMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<EventualConsistencyMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, IHolderDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();

        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (EventualConsistencyException ex)
            {
                // TODO: NOTIFY THE CLIENT THAT EVEN THOUGH THEY GOT A GOOD RESPONSE, THE CHANGES DIDN'T TAKE PLACE DUE TO AN UNEXPECTED ERROR
                _logger.LogError("Eventual consistency failure. Error code: {errorCode}, description: {errorDescription}. The operation was committed, but subsequent consistency actions failed.", ex.EventualConsistencyError.Code, ex.EventualConsistencyError.Description);
            }
            catch (Exception ex)
            {
                // TODO: NOTIFY THE CLIENT THAT EVEN THOUGH THEY GOT A GOOD RESPONSE, THE CHANGES DIDN'T TAKE PLACE DUE TO AN UNEXPECTED ERROR
                _logger.LogCritical("Unexpected error during request finalization. The operation was committed, but an error occurred while processing subsequent actions. Error: {exceptionMessage}. Immediate investigation required to prevent data inconsistencies.", ex.Message);
            }
            finally
            {
                await transaction.DisposeAsync();
            }

        });

        await _next(context);
    }
}