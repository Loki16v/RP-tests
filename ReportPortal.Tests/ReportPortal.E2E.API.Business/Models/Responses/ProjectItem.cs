using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses
{
    public class ProjectItem
    {
        [JsonPropertyName("id")]
        public int ProjectId { get; set; }

        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; }

        [JsonPropertyName("usersQuantity")]
        public int UsersQuantity { get; set; }

        [JsonPropertyName("launchesQuantity")]
        public int LaunchesQuantity { get; set; }

        [JsonPropertyName("launchesPerUser")]
        public string LaunchesPerUser { get; set; }

        [JsonPropertyName("uniqueTickets")]
        public string UniqueTickets { get; set; }

        [JsonPropertyName("launchesPerWeek")]
        public string LaunchesPerWeek { get; set; }

        [JsonPropertyName("creationDate")]
        public long CreationDate { get; set; }

        [JsonPropertyName("entryType")]
        public string EntryType { get; set; }

        [JsonPropertyName("organization")]
        public string Organization { get; set; }


    }
}
