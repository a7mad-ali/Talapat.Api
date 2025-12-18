using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Talapat.Api.Errors;

namespace Talapat.Api.Middleware
{
    //by convension
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //take and action with the request

                await _next.Invoke(httpContext); //go to the next middleware

                // take and action with response 
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message); //development

                //log exception in (database | files) production env

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                var option = new JsonSerializerOptions() { PropertyNamingPolicy=JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response,option);


                await httpContext.Response.WriteAsync(json);

                 

            }
        }
    }
}
