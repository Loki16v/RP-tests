using System.Text.Json.Serialization;

namespace ReportPortal.E2E.Core.Models
{
    public class TokenInformation
    {
        public TokenInformation() => TokenTimeReceiving = DateTime.UtcNow;

        public DateTime TokenTimeReceiving { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
