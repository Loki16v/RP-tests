using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Core.Business.Models.Responses
{
    public class CreateProjectResponse
    {
        [JsonPropertyName("id")]
        public int ProjectId { get; set; }
    }
}
