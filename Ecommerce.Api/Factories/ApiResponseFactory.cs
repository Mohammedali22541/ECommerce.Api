using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, x => x.Value.Errors.Select(
                    x => x.ErrorMessage).ToArray());
            var problem = new ProblemDetails()
            {
                Type = "Validation Errors",
                Detail = "One Or More Validations Errors Have Occurred",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { "Errors" , errors } }
            };

            return new BadRequestObjectResult(problem);
        }
    }
}
