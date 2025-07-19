using VendingMachine.Application.Models;
using VendingMachine.Domain.Shared;

namespace VendingMachine.API.Middlewares;

public class SingleUserMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly SemaphoreSlim _lock = new(1, 1);
    private static DateTime _lastAccessTime = DateTime.MinValue;

    public SingleUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _lock.WaitAsync();
        try
        {
            if (_lastAccessTime.AddMinutes(2) > DateTime.UtcNow && 
                !context.Session.Keys.Contains("UserActive"))
            {
                var error = Errors.Session.SessionIsBusy();
                var envelope = Envelope.Error(error);
            
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsJsonAsync(envelope);
                return;
            }

            context.Session.SetString("UserActive", "true");
            _lastAccessTime = DateTime.UtcNow;
        }
        finally
        {
            _lock.Release();
        }

        await _next(context);
    }
}

public static class SingleUserMiddlewareExtensions
{
    public static IApplicationBuilder UseSingleUserMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SingleUserMiddleware>();
    }
}