using ApplicationCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieShopAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;
        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //read info from t he HttpContext Object and log it some where

            try
            {
                _logger.LogInformation("Inside the MiddleWare");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //First catch the exception
                //check the exception message
                //check stacktrace,where happened
                //when the exception hapned
                // for which url and which http method happened(controller, action method_
                //for which user, if user is logged in

                //save all these info somewhere, text files json files or Database
                //System.IO to create textfile(option1)

                //But we use this
                //asp.net core has builtin loggin mechanism (ILogger) which can be used by any third party log provide
                // *Serilog* and Nlog

                //send email to Dev Team when exception happened 

                ErrorModel exceptionDetails = new ErrorModel
                {
                    Message = ex.Message,
                   // StackTrace = ex.StackTrace,
                    ExceptionDateTime = DateTime.UtcNow,
                    ExceptionType = ex.GetType().ToString(),
                    Path = httpContext.Request.Path,
                    HttpRequestType = httpContext.Request.Method,
                    User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name: null
                };

                _logger.LogError("Exception happened, log this to text or Json files using SeriLog");

                //return http status code 500

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize<ErrorModel>(exceptionDetails);

                await httpContext.Response.WriteAsync(result);

                //httpContext.Response.Redirect("/home/error") if this is MVC

            }

           


        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        //extension method on IApplicationBuilder
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
