using System.Net.Http.Json;

namespace ReportPortal.E2E.Core.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static TResult GetResponse<TResult>(this HttpResponseMessage message) =>
            message.Content.ReadFromJsonAsync<TResult>().Result;
    }
}
