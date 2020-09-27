using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;

namespace Presentation.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke (HttpContext context)
        {
            try
            {
                await _next(context);
            } 
            catch (Exception exception)
            {

                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;
            var responseResult = string.Empty;

            switch(exception)
            {
                case RequestValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new RestError(validationException.Message,
                        validationException.Failures));
                    break;
                case ValidationException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case IdentityException identityException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new RestError(identityException.Message, identityException.Failures));
                    break;
                case CreateException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case InvalidCredentialsException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case AlreadyExistsException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case HttpRequestException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case InvalidOperationException _:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new RestError(exception.Message));
            }

            if (string.IsNullOrEmpty(responseResult))
            {
                responseResult = result;
            }

            _logger.LogWarning(exception,
                "Exception handled at [{nameof}] [{User}]: API response code=[{code}], message=[{message}]",
                nameof(CustomExceptionHandlerMiddleware),
                context.User?.Claims?.
                    Where(c => c.Type == ClaimTypes.Email).
                    Select(v => v.Value).
                    FirstOrDefault(),
                code, result);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(responseResult);
        }
    }
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
