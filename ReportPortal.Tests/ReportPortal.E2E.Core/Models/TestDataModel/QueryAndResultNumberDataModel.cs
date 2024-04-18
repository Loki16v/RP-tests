using System.Text.Json.Serialization;

namespace ReportPortal.E2E.Core.Models.TestDataModel
{
    public class QueryAndResultNumberDataModel
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }
    }
}
