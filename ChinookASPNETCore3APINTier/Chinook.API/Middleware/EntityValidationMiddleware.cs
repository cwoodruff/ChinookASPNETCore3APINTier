using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Chinook.API.Middleware
{
    public static class UserKeyValidatorsExtension
    {
        public static IApplicationBuilder ApplyEntityValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<EntityValidationMiddleware>();
            return app;
        }
    }
    
    public class EntityValidationMiddleware
    {
        private readonly RequestDelegate _next;       
        public EntityValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.GetDisplayUrl().Contains("swagger"))
                await _next.Invoke(context);
            else
            {
                switch (context.Request.Method)
                {
                    case "GET" when !context.Request.Headers.Keys.Contains("id"):
                        context.Response.StatusCode = 400; //Bad Request                
                        await context.Response.WriteAsync("Id is missing");
                        return;
                    case "POST" when !context.Request.ContentLength.Equals(0):
                        context.Response.StatusCode = 400; //UnAuthorized
                        await context.Response.WriteAsync("Entity missing");
                        return;
                    case "PUT" when !context.Request.Headers.Keys.Contains("id") ||
                                    !context.Request.ContentLength.Equals(0):
                        context.Response.StatusCode = 400; //UnAuthorized
                        await context.Response.WriteAsync("Entity missing");
                        return;
                    case "DELETE" when !context.Request.Headers.Keys.Contains("id"):
                        context.Response.StatusCode = 400; //UnAuthorized
                        await context.Response.WriteAsync("Id is missing");
                        return;
                    default:
                        await _next.Invoke(context);
                        break;
                }
            }
        }
    }
}