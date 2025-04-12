using Ecom.Api.Hellper;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json;

namespace Ecom.Api.Middlrware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _RateLimitWindow=TimeSpan.FromSeconds(30);
        public ExceptionsMiddleware(RequestDelegate request, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _request = request;
            _environment = environment;
            _memoryCache = memoryCache;
        }

        public async Task Invoke(HttpContext context) 
        {
            ApplySecurity(context);
            if (IsRequestedAllowed(context)==false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.ContentType = "application/json";
                var response = new ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too Many Request, please try again Later");
                 await context.Response.WriteAsJsonAsync(response);
            }
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                 context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                context.Response.ContentType="application/json";

                var response =_environment.IsDevelopment()?
                    new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message,ex.StackTrace)
                    : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);

                var Json=JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(Json);

            }
        }

        private bool IsRequestedAllowed(HttpContext context)
        {

            var ip=context.Connection.RemoteIpAddress;
            var CashKey = $"Rate:{ip}";
            var DateNow= DateTime.Now;

            var (timestamp, count) = _memoryCache.GetOrCreate(CashKey, entry => {
                entry.AbsoluteExpirationRelativeToNow = _RateLimitWindow;
                return (timestamp: DateNow, count: 0);
                });
            if (DateNow-timestamp<_RateLimitWindow)
            {
                if (count > 8)
                {
                   return false;
                }
                _memoryCache.Set(CashKey, (timestamp, count+=1), _RateLimitWindow);
            }
            else
            {
                _memoryCache.Set(CashKey, (timestamp, count), _RateLimitWindow);
            }
            return true;


        }
        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
