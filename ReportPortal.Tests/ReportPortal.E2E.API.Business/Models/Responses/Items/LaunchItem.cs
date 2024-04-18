using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses.Items
{
    public class LaunchItem
    {
        [JsonPropertyName("owner")]
        public string Owner { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public long EndTime { get; set; }

        [JsonPropertyName("lastModified")]
        public long LastModified { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}