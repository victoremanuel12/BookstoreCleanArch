namespace Domain.Abstraction
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
            Error = Error.None;
        }
        public static Result<T> Succss(T value) => new (value);
        public static Result<T> Failaure(Error error) => new (error);
    }
    
    
}
