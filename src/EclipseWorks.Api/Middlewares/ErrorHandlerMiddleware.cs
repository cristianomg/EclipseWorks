using EclipseWorks.Application.Handlers.Commands;
using EclipseWorks.Domain.Exceptions;
using System.IO;
using System.Net;
using System.Text.Json;

namespace EclipseWorks.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ForbiddenException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.Forbidden); }
            catch (CommentRequiredException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (PendingTaskException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (ProjectNameInUseException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (NotFoundException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotFound); }
            catch (TaskLimitExceededException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotAcceptable); }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }

        private Task HandleCustomExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }

    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
