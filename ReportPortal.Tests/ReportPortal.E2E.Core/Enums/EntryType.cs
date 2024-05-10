using System.Text.Json.Serialization;

namespace ReportPortal.E2E.Core.Enums
{
    public enum EntryType
    {
        [JsonPropertyName("PERSONAL")]
        Personal,
        [JsonPropertyName("INTERNAL")]
        Internal
    }
}
