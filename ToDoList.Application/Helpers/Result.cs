using System;

namespace ToDoList.Application.Helpers
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public string ResultMessage { get; set; }
        public int ResultCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorDetails { get; set; } // Optional error details for failures

        // Success with value
        public static Result<T> Success(int resCode, string message, T value)
            => new Result<T> { Value = value, ResultMessage = message, ResultCode = resCode, IsSuccess = true };

        // Success without value (EmptyResult)
        public static Result<EmptyResult> Success(int resCode, string message)
            => new Result<EmptyResult> { ResultMessage = message, ResultCode = resCode, IsSuccess = true };

        // Failure without value
        public static Result<T> Failure(int resCode, string message)
            => new Result<T> { ResultMessage = message, ResultCode = resCode, IsSuccess = false };

        // Failure with error details
        public static Result<EmptyResult> Failure(int resCode, string message, string errorDetails)
            => new Result<EmptyResult> { ResultMessage = message, ResultCode = resCode, IsSuccess = false, ErrorDetails = errorDetails };

        // Generic Failure (with value)
        public static Result<T> Failure(int resCode, string message, T value)
            => new Result<T> { Value = value, ResultMessage = message, ResultCode = resCode, IsSuccess = false };

        // Failure with value and error details
        public static Result<T> Failure(int resCode, string message, string errorDetails, T value)
            => new Result<T> { Value = value, ResultMessage = message, ResultCode = resCode, IsSuccess = false, ErrorDetails = errorDetails };
    }
}
