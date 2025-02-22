using System.Text.Json.Serialization;

namespace minimarket_project_backend.Models.Responses
{
    public class DataResponse<T> : ApiResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; } = default!;
    }
}
