using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses
{
    public class GetLaunchesResponse
    {
        [JsonPropertyName("content")]
        public List<LaunchItem> Launches { get; set; }

        [JsonPropertyName("page")]
        public ResponseInfoItem ResponseInfo { get; set; }
    }
}
