namespace EcommerceSolution.BackendAPI.Common
{
    public class ApiValidationErrors<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }
        
        
        public ApiValidationErrors(string[] validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}
