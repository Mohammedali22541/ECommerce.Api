using ECommerce.Shared.CommonResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ApiBaseController: ControllerBase
    {
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();

            else
                return HandleProblem(result.Errors);
        }

        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            else
                return HandleProblem(result.Errors);
        }

        protected ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            // No Error - 500 Server Error
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError , title: "An Error Occurred");
            // Validation Errors

            if (errors.All(e=>e.ErrorType == ErrorType.Validation))
            {
                return HandleValidationErrors(errors);
            }
            // Single Error

            return HandleSingleError(errors[0]);
        }

        protected ActionResult HandleSingleError(Error error)
        {
            return Problem(detail: error.Description, title: error.Code, type: error.ErrorType.ToString() , 
                statusCode : MapErrorTypeIntoStatusCode(error.ErrorType)
                );
            
        }

        protected ActionResult HandleValidationErrors(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelState);
        }

        protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email)!;
        private static int MapErrorTypeIntoStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
