using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FormAPI.Middleware
{
    public class ContentTypeMiddleware
    {
        private readonly RequestDelegate _next;

        public ContentTypeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.ContentType == "application/json; charset=utf-8")
                {
                    context.Response.ContentType = "application/vnd.api+json";
                }
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
