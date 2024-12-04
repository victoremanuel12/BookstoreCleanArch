namespace Domain.Abstraction
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }

        public Error? Error { get; }

        protected Result(T value)
        {
            IsSuccess = true;
            Data = value;
            Error = null;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result<T> Success(T data) => new(data);
        public static Result<T> Failure(Error error) => new(error);


    }
}
