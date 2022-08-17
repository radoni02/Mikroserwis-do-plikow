using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        public readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            try
            {
                await _next(context);
            }
            catch (ProjectException ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = ex._errorCode;
                context.Response.Headers.Add("content-type", "application/json");

                var errorCode = ex._errorCode;
                var json = JsonSerializer.Serialize(new { errorCode = errorCode, ex.Message });
                await context.Response.WriteAsync(json);
            }
        }
    }
}
