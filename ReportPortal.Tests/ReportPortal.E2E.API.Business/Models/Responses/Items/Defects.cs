using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses.Items
{
    public class Defects
    {
        [JsonPropertyName("system_issue")]
        public DefectType SystemIssue { get; set; }

        [JsonPropertyName("product_bug")]
        public DefectType ProductBug { get; set; }

        [JsonPropertyName("to_investigate")]
        public DefectType ToInvestigate { get; set; }

        [JsonPropertyName("automation_bug")]
        public DefectType AutomationBug { get; set; }
    }
}
