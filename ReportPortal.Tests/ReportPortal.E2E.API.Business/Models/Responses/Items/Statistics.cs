using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses.Items
{
    public class Statistics
    {
        [JsonPropertyName("executions")]
        public Execution Executions { get; set; }

        [JsonPropertyName("defects")]
        public Defects Defects { get; set; }
    }
}
