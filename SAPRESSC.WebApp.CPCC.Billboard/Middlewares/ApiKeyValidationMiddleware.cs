using Microsoft.AspNetCore.Http;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Middlewares
{
    public sealed class ApiKeyValidationMiddleware
    {
        private const string API_KEY_HEADER_PROPERTY = "X-Api-Key";

        private readonly RequestDelegate next;

        private readonly Microsoft.Extensions.Options.IOptions<Options.ApiSecurityOptions> apiSecurityOptions;

        public ApiKeyValidationMiddleware(RequestDelegate next, Microsoft.Extensions.Options.IOptions<Options.ApiSecurityOptions> apiSecurityOptions)
        {
            this.next = next;
            this.apiSecurityOptions = apiSecurityOptions;
        }

        public async System.Threading.Tasks.Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(new PathString("/api/mediastore")))
            {
                if (!context.Request.Headers.Keys.Contains(API_KEY_HEADER_PROPERTY) || !context.Request.Headers[API_KEY_HEADER_PROPERTY][0].Equals(apiSecurityOptions.Value.TerminalMediaStoreKey))
                {
                    context.Response.StatusCode = (int)System.Net.HttpStatusCode.UpgradeRequired;

                    await context.Response.WriteAsync("Upgrade Required!");

                    return;
                }

                await next.Invoke(context);

                return;
            }

            if (context.Request.Path.StartsWithSegments(new PathString("/api")) && (!context.Request.Headers.Keys.Contains(API_KEY_HEADER_PROPERTY) || !context.Request.Headers[API_KEY_HEADER_PROPERTY][0].Equals(apiSecurityOptions.Value.TerminalProjectKey)))
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.UpgradeRequired;

                await context.Response.WriteAsync("Upgrade Required!");
                
                return;
            }

            await next.Invoke(context);
        }
    }
}
