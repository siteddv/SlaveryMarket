using System.Net;
using System.Text.Json;
using SlaveryMarket.Helpers;

namespace SlaveryMarket;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = Result<object>.InternalServerError(ex.Message);
            
            var json = JsonSerializer.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}