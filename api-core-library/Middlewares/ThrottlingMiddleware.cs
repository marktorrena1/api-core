using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace api_core_library.Middlewares;

public class ThrottlingMiddleware
{
 private readonly RequestDelegate _next;
    private readonly ILogger<ThrottlingMiddleware> _logger;
    private readonly IMemoryCache _cache;
    private const int RequestLimit = 10;
    private static readonly TimeSpan TimeWindow = TimeSpan.FromMinutes(1);

    public ThrottlingMiddleware(RequestDelegate next, ILogger<ThrottlingMiddleware> logger, IMemoryCache cache)
    {
        _next = next;
        _logger = logger;
        _cache = cache;
    }

    public async Task Invoke(HttpContext context)
    {
        var userIdentifier = context.User?.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString();

        if (userIdentifier != null)
        {
            var cacheKey = $"RequestCount_{userIdentifier}";
            var requestCount = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.SlidingExpiration = TimeWindow;
                return new List<DateTime>();
            });

            requestCount.Add(DateTime.UtcNow);
            requestCount.RemoveAll(time => time < DateTime.UtcNow - TimeWindow);

            if (requestCount.Count > RequestLimit)
            {
                _logger.LogWarning("Request limit exceeded for user {UserIdentifier}", userIdentifier);
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            _cache.Set(cacheKey, requestCount);
        }

        await _next(context);
    }
}
