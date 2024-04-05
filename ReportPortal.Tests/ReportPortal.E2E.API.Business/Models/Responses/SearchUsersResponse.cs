using System.Text.Json.Serialization;

namespace ReportPortal.E2E.API.Business.Models.Responses
{
    public class SearchUsersResponse
    {
        [JsonPropertyName("content")]
        public List<UserItem> UserList { get; set; }

        [JsonPropertyName("page")]
        public ResponseInfoItem ResponseInfo { get; set; }
    }
}
