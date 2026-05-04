using mini_shop_backend_net.Application.Common;
using mini_shop_backend_net.Application.Common.Exceptions;

namespace mini_shop_backend_net.Middleware;

using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
        var statusCode = HttpStatusCode.InternalServerError;
        var errorCode = "INTERNAL_ERROR";
        var message = "Внутренняя ошибка сервера";

        if (ex is AppException appEx)
        {
            statusCode = (HttpStatusCode)appEx.StatusCode;
            errorCode = "BUSINESS_ERROR";
            message = appEx.Message;
        }

        var response = new ErrorResponse
        (
            errorCode,
            message,
            DateTime.UtcNow,
            context.Request.Path
        );

        var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);
    }
}