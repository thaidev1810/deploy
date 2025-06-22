using BloodDonation.Apis.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace BloodDonation.Apis.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();
        return app;
    }
}