namespace Domain.Abstraction
{
    public  record Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorType Type { get; set; }

        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        private  Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description; 
            Type = type;
        }
        public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);
        public static Error Validation(string code, string description) => new(code, description, ErrorType.Validation);
        public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
        public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);

        //public static implicit operator  Result(Error error) => Result.Failaure(error);
    }
}
