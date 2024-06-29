namespace tienda_project_backend.Utilities.Response
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public string? Error { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}
