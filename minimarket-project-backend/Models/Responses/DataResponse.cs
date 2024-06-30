namespace minimarket_project_backend.Models.Responses
{
    public class DataResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }
}
