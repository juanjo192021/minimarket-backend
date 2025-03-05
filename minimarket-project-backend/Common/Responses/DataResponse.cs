using System.Text.Json.Serialization;

namespace minimarket_project_backend.Common.Responses
{
    public class DataResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; } = default!;
    }
}
