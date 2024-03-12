namespace FiapBank.WebApi.Contracts.Responses
{
    public class ResultResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public ResultResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public ResultResponse(bool success)
        {
            Success = success;
        }

        public ResultResponse()
        {
        }

        public static ResultResponse Ok(string message)
        {
            return new ResultResponse(true, message);
        }

        public static ResultResponse Ok()
        {
            return new ResultResponse(true);
        }

        public static ResultResponse Fail(string message)
        {
            return new ResultResponse(false, message);
        }

        public static ResultResponse Fail()
        {
            return new ResultResponse(false);
        }
    }

    public class ResultResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ResultResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public ResultResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public ResultResponse(bool success, T data)
        {
            Success = success;
            Data = data;
        }

        public ResultResponse(bool success)
        {
            Success = success;
        }

        public ResultResponse()
        {
        }

        public static ResultResponse<T> Ok(T data)
        {
            return new ResultResponse<T>(true, data);
        }

        public static ResultResponse<T> Ok(string message, T data)
        {
            return new ResultResponse<T>(true, message, data);
        }

        public static ResultResponse<T> Ok(string message)
        {
            return new ResultResponse<T>(true, message);
        }

        public static ResultResponse<T> Ok()
        {
            return new ResultResponse<T>(true);
        }

        public static ResultResponse<T> Fail(string message)
        {
            return new ResultResponse<T>(false, message);
        }

        public static ResultResponse<T> Fail()
        {
            return new ResultResponse<T>(false);
        }

        public static ResultResponse<T> Fail(T data)
        {
            return new ResultResponse<T>(false, data);
        }

        public static ResultResponse<T> Fail(string message, T data)
        {
            return new ResultResponse<T>(false, message, data);
        }

        public static implicit operator ResultResponse<T>(T data)
        {
            return new ResultResponse<T>(true, data);
        }
    }
}
