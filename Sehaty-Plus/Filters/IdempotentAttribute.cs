using Microsoft.AspNetCore.Mvc.Filters;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Filters
{
    public class IdempotentAttribute : IAsyncResourceFilter
    {
        private const string IdempotencyKeyHeader = "X-Idempotency-Key";
        private class CacheEntry
        {
            public int StatusCode { get; set; }
            public object Result { get; set; } = null!;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(IdempotencyKeyHeader, out var idempotencyKey) || string.IsNullOrEmpty(idempotencyKey))
            {
                context.Result = new BadRequestObjectResult($"Missing {IdempotencyKeyHeader} header.");
                return;
            }
            var cacheKey = $"IdempotencyKey:{idempotencyKey}";
            var cache = context.HttpContext.RequestServices.GetService<ICacheService>();

            var cachedkey = await cache!.GetAsync<CacheEntry>(cacheKey);

            if (cachedkey is not null)
            {
                context.Result = new ObjectResult(cachedkey.Result)
                {
                    StatusCode = cachedkey.StatusCode
                };

                return;
            }
            else
            {
                var executedContext = await next();
                if (executedContext.Result is ObjectResult objectResult && objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
                {
                    var cacheEntry = new CacheEntry
                    {
                        StatusCode = objectResult.StatusCode ?? 200,
                        Result = objectResult.Value!
                    };
                    await cache.SetAsync(cacheKey, cacheEntry);
                }
            }






            await next();
        }
    }
}
