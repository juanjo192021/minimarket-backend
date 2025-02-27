using System.Text.Json.Serialization;

namespace minimarket_project_backend.Common.Responses
{
    public class DataResponse<T> : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; } = default!;
    }
}
