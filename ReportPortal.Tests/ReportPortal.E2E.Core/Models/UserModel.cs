using System.Text.Json.Serialization;

namespace ReportPortal.E2E.Core.Models
{
    public class UserModel
    {
        [JsonPropertyName("accountRole")]
        public string AccountRole { get; set; }

        [JsonPropertyName("defaultProject")]
        public string DefaultProject { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("projectRole")]
        public string ProjectRole { get; set; }
    }
}