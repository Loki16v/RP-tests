using System.Text.Json.Serialization;
using ReportPortal.E2E.API.Business.Models.Responses.Items;

namespace ReportPortal.E2E.API.Business.Models.Responses
{
    public class GetDashboardsResponse
    {
        [JsonPropertyName("content")]
        public List<DashboardItem> Dashboards { get; set; }

        [JsonPropertyName("page")]
        public ResponseInfoItem ResponseInfo { get; set; }
    }
}
