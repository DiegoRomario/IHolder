using IHolder.Domain.Common;
using IHolder.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IHolder.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

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
            catch (Exception)
            {
                //  TODO: NOTIFY THE CLIENT THAT EVEN THOUGH THEY GOT A GOOD RESPONSE, THE CHANGES DIDN'T TAKE PLACE DUE TO AN UNEXPECTED ERROR
            }
            finally
            {
                await transaction.DisposeAsync();
            }

        });

        await _next(context);
    }
}