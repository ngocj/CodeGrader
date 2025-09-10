namespace Common
{
    public class Result<T>
    {
        public Result(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ErrorDetail errorDetail { get; set; }

        public static  Result<T> Failure(string message)
        {
            return new Result<T>(false, message, default);
        }
        public static Result<T> Failure(List<ErrorField> errorFields) 
        {
            return new Result<T>(false, "Invalid validation failed", default)
            {
                errorDetail = new ErrorDetail { errorFields = errorFields }
            };
        }
        public static Result<T> Success(T data,string message)
        {
            return new Result<T>(true, message, data);
        }

    }
    public class ErrorField
    {
        public string Field { get; set; }
        public string errorMessage { get; set; }
    }
    public class ErrorDetail
    {
        public List<ErrorField> errorFields { get; set; } = new List<ErrorField>();
    }
}
