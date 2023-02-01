using Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Http;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string result;

            if (ex.GetType() == typeof(ValidationException))
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var exception = (ValidationException)ex;
                result = JsonSerializer.Serialize(new { message = exception.Errors?.Values });
            }
            else if (ex.GetType() == typeof(NotFoundException))
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                var exception = (NotFoundException)ex;
                result = JsonSerializer.Serialize(new { message = exception.Message });
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(new { message = ex?.Message });
            }

            await response.WriteAsync(result);
        }
    }
}
