using ECommerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.CustomMiddlewares
{
    public class ExceptionHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next , ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound && !httpContext.Response.HasStarted)
                {
                    var problem = new ProblemDetails()
                    {
                        Title = "Error While Processing HTTP Request -End Point Not Found",
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"End Point {httpContext.Request.Path} Not Found",
                        Instance = httpContext.Request.Path

                    };
                    await httpContext.Response.WriteAsJsonAsync(problem);
                }

            }
            catch (Exception ex)
            {
                // Log Error
                _logger.LogError(ex.Message , "Something Went Wrong");

                // Return Custome Error Response

                var Problem = new ProblemDetails()
                {
                    Title = "An Unexpected Error Occured",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError,
                    }


                };
                httpContext.Response.StatusCode = Problem.Status.Value;


                await httpContext.Response.WriteAsJsonAsync(Problem);
            }
        }
    }
}
