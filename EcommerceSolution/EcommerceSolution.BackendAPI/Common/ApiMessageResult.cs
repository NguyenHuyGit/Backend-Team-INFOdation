namespace EcommerceSolution.BackendAPI.Common
{
    public class ApiMessageResult<T> : ApiResult<T>
    {
        public ApiMessageResult()
        {
        }
        public ApiMessageResult(bool status, string message)
        {
            IsSuccessed = status;
            Message = message;
        }
    }
}
