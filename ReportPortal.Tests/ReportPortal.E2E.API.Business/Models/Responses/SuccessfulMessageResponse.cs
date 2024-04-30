using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses
{
    public class SuccessfulMessageResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
