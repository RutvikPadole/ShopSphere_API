using Microsoft.AspNetCore.Http;
using ShopSphere_API.Entities;
namespace ShopSphere_API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {

        }
    }
}


