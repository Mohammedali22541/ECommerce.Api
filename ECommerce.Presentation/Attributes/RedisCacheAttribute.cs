using ECommerce.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMins;

        public RedisCacheAttribute(int durationInMins)
        {
            _durationInMins = durationInMins;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get CacheService From Dependancy Injection
            var cashService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            // Create CacheKey Based On RequestPath & QueryParams
            var cacheKey = CreateCache(context.HttpContext.Request);

            // Check If Data Exist In Cache
            var cacheValue = await cashService.GetAsync(cacheKey);

            // If Exist , Return Cached Data And Skip Excuting Endpoint

            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }

            // If Not Exist , Excute Endpoint and Store Result In Cache If Response Returned 200 Ok

            var ExcutedContent = await next.Invoke();

            if (ExcutedContent.Result is OkObjectResult result)
            {
                await cashService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(_durationInMins));
            }
        }

        private string CreateCache(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();

            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x=>x.Key))
            {
                key.Append($"/{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }

}
