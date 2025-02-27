using System.Text.Json.Serialization;

namespace minimarket_project_backend.Common.Responses
{
    public class ErrorResponse: BaseResponse
    {

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Error { get; set; } = null!;
        
    }
}
