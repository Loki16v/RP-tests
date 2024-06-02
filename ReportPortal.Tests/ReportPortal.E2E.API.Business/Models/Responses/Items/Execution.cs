using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses.Items
{
    public class Execution
    {

        [JsonPropertyName("total")]
        public int Total { get; set; }


        [JsonPropertyName("passed")]
        public int Passed { get; set; }


        [JsonPropertyName("failed")]
        public int Failed { get; set; }


        [JsonPropertyName("skipped")]
        public int Skipped { get; set; }
    }
}
