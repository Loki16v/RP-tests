using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses.Items
{
    public class DefectType
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
