using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses.Items
{
    public class ResponseInfoItem
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("totalElements")]
        public int TotalElements { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }
}