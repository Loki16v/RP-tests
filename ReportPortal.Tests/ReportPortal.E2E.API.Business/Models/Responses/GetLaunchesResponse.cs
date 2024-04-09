using System.Text.Json.Serialization;
using ReportPortal.E2E.API.Business.Models.Responses.Items;

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