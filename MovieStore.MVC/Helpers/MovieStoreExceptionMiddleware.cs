using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MovieStore.MVC.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project

    // Always make sure you have ExceptionMiddlewares register at the very beginning

    public class MovieStoreExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<MovieStoreExceptionMiddleware> _logger;

        public MovieStoreExceptionMiddleware(RequestDelegate next, ILogger<MovieStoreExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation("MovieStoreExceptionMiddleware is called");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception happened: {ex}");
                await HandleException(httpContext, ex);
            }

            //return _next(httpContext);
        }

        public async Task HandleException(HttpContext context, Exception exception)
        {
            // Step 1: log the exception details, such as:           
            // 1. Exception message
            // 2. Exception Stacktrace
            // 3. the Date time exception happened
            // 4. The User info
            _logger.LogInformation("------------Start of logging------------");
            _logger.LogError($"Exception Message: {exception.Message}");
            _logger.LogError($"Exception Stack Trace: {exception.StackTrace}");
            _logger.LogInformation($"Exception for User: {context.User.Identity.Name}");
            _logger.LogInformation($"Exception happened on: {DateTime.UtcNow}");
            _logger.LogInformation("------------End of logging------------");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;



            // Step 2: Send notification(email preferred) to the Dev team
            // using MailKit for free -- send emails
            // SendGrid - paid

            // Step 3: Show a friendly error page to the User
            context.Response.Redirect("/Home/Error");
            await Task.CompletedTask;

            // Popular logging framework: Serilog (preferred)
            // NLog and Log4net

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieStoreExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieStoreExceptionMiddleware>();
        }
    }
}
