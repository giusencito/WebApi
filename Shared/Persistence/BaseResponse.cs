namespace WebApi.Shared.Persistence
{
    public class BaseResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Resource { get; private set; }
        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }

        public BaseResponse(T resource)
        {
            Success = true;
            Resource = resource;
        }
    }
}
