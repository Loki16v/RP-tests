using System.Text.Json.Serialization;
using ReportPortal.E2E.API.Business.Models.Responses.Items;

namespace ReportPortal.E2E.API.Business.Models.Responses
{
    public class ProjectsListResponse
    {
        [JsonPropertyName("content")]
        public List<ProjectItem> ProjectsList { get; set; }

        [JsonPropertyName("page")]
        public ResponseInfoItem ResponseInfo { get; set; }
    }
}