using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommonResponse
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorType ErrorType { get; set; }

        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public static Error Failure (string code = "General Failure", string description = "A General Failure has Occurred")
        {
            return new Error(code, description, ErrorType.Failure);
        }

        public static Error Validation (string code = "Validation Error", string description = "A Validation Error has Occurred")
        {
            return new Error(code, description, ErrorType.Validation);
        }

        public static Error NotFound (string code = "Not Found", string description = "The Requested Resource was Not Found")
        {
            return new Error(code, description, ErrorType.NotFound);
        }

        public static Error Unauthorized (string code = "Unauthorized", string description = "Unauthorized Access")
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }

        public static Error Forbidden (string code = "Forbidden", string description = "Access to the Resource is Forbidden")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }

        public static Error InvalidCredentials (string code = "Invalid Credentials", string description = "Invalid Credentials Provided")
        {
            return new Error(code, description, ErrorType.InvalidCredentials);
        }

    }
}
