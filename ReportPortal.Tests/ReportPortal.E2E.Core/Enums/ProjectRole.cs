using System.Text.Json.Serialization;

namespace ReportPortal.E2E.Core.Enums
{
    public enum ProjectRole
    {
        [JsonPropertyName("PROJECT_MANAGER")]
        ProjectManager,
        [JsonPropertyName("MEMBER")]
        Member,
        [JsonPropertyName("OPERATOR")]
        Operator,
        [JsonPropertyName("CUSTOMER")]
        Customer
    }
}
