using System.Text.Json.Serialization;

namespace ReportPortal.E2E.Core.Enums
{
    public enum AccountRole
    {
        [JsonPropertyName("USER")]
        User,
        [JsonPropertyName("ADMINISTRATOR")]
        Administrator
    }
}
