﻿using Domain.Abstraction;

namespace Api.Extensions
{
    public static class ResultExtensions
    {
        public static IResult MapResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            return GetErrorResult(result.Error);
        }

        private static IResult GetErrorResult(Error error)
        {
            return error.Type switch
            {
                ErrorType.Validation => Results.BadRequest(error),
                ErrorType.Conflict => Results.Conflict(error),
                ErrorType.NotFound => Results.NotFound(error),
                _ => Results.Problem(
                    statusCode:500,
                    title:"Server Failure",
                    type:Enum.GetName(typeof(ErrorType), error.Type),
                    extensions: new Dictionary<string, object?>
                        {
                            {"errors", new []{error} }
                        }),

            };
        }
    }
}
