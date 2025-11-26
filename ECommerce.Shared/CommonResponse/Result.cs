using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerce.Shared.CommonResponse
{
    public class Result
    {
        private readonly List<Error> _errors = [];
        public bool IsSuccess => _errors.Count == 0;
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => _errors;

        // ok 
        protected Result()
        {

        }

        // Error
        protected Result(Error error)
        {
            _errors.Add(error);
        }
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }


        public static Result Ok() => new Result();

        public static Result Fail(Error error) => new Result(error);
        public static Result Fail(List<Error> errors) => new Result(errors);






    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value => IsSuccess? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");
        private Result(TValue value)
        {
            _value = value;
        }
        private Result(Error error):base (error)
        {
            _value = default!;
        }
        public Result(List<Error> errors): base(errors) 
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new(value);
        public static new Result<TValue> Fail(Error error) => new(error);
        public static new Result<TValue> Fail(List<Error> errors) => new(errors);


        public static implicit operator Result<TValue>(TValue value) =>  Result<TValue>.Ok(value);
        public static implicit operator Result<TValue>(Error error) => Result<TValue>.Fail(error);
        public static implicit operator Result<TValue>(List<Error> errors) => Result<TValue>.Fail(errors);

    }
}
