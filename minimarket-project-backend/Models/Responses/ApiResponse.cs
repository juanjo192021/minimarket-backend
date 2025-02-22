using System.Text.Json.Serialization;

namespace minimarket_project_backend.Models.Responses
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Error { get; set; } = null!;
        public bool Success { get; set; }
    }
}
