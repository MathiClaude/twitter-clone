namespace TwitterClone.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        
        public int StatusCode {get; private set;}

        protected Result(bool isSuccess, string error, int statusCode = 200)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result Failure(string error, int statusCode = 500)
        {
            return new Result(false, error, statusCode);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        private Result(bool isSuccess, string error, T value, int statusCode = 200) 
            : base(isSuccess, error, statusCode)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, string.Empty, value);
        }

        public static Result<T> Failure(string error, int statusCode = 500)
        {
            return new Result<T>(false, error, default, statusCode);
        }
    }
}