using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses
{
    public class ErrorResponse
    {
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }


        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
