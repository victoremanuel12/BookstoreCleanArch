﻿namespace Domain.Abstraction
{
    public class Result<T>
    {
        public bool IsSuccess {get;}
        public T? Value { get;}
        public Error Error { get;}

        private Result( T value)
        {
            IsSuccess = true;
            Value = value;
            Error = Error.None;
        }
        private Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }
        public static Result<T> Success(T value) => new (value);
        public static Result<T > Failure(Error error) => new (error);
    }
    
    
}
