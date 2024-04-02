using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Core.Business.Models.Responses
{
    public class ProjectsListResponse
    {
        [JsonPropertyName("content")]
        public List<ProjectItem> ProjectsList { get; set; }

        [JsonPropertyName("page")]
        public ResponseInfoItem ResponseInfo { get; set; }
    }
}
