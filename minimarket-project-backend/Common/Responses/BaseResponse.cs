namespace minimarket_project_backend.Common.Responses
{
    public abstract class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public bool Success { get; set; }
    }
}
