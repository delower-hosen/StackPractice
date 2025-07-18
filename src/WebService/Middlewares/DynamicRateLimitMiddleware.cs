using Shared.Attributes;

namespace WebService.Middlewares
{
    public class DynamicRateLimitMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private static readonly Dictionary<string, (int Count, DateTime ResetTime)> RequestCounters = [];

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var rateLimitedAttr = endpoint?.Metadata.GetMetadata<RateLimitAttribute>();

            if (rateLimitedAttr != null)
            {
                var periodSeconds = ParsePeriod(rateLimitedAttr.Period);
                var key = $"{context.Connection.RemoteIpAddress}:{endpoint?.DisplayName}";

                if (!RequestCounters.TryGetValue(key, out var entry) || entry.ResetTime < DateTime.UtcNow)
                {
                    entry = (0, DateTime.UtcNow.AddSeconds(periodSeconds));
                }

                if (entry.Count >= rateLimitedAttr.Limit)
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Too many requests. Please wait a moment before trying again.");
                    return;
                }

                RequestCounters[key] = (entry.Count + 1, entry.ResetTime);
            }

            await _next(context);
        }

        private static double ParsePeriod(string period)
        {
            if (string.IsNullOrWhiteSpace(period)) return 60;

            var unit = period[^1];
            var value = double.Parse(period[..^1]);

            return unit switch
            {
                's' => value,
                'm' => value * 60,
                'h' => value * 3600,
                _ => 60 
            };
        }

    }

}
