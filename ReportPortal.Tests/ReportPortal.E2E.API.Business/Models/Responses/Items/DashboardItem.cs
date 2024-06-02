using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses.Items
{
    public class DashboardItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
