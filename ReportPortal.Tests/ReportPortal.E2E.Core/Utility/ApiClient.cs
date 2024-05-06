using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.HttpMessageHandlers;
using ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient;
using HttpClient = ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient.HttpClient;

namespace ReportPortal.E2E.Core.Utility
{
    public static class ApiClient
    {
        public static IHttpClient Get(string baseUrl, AuthorizationMessageHandler authorizationMessageHandler = null)
        {
            var clientName = TestsBootstrap.Instance.Configuration.GetSection("ApiClient").GetValueOrThrow();
            return clientName switch
            {
                "HttpClient" => authorizationMessageHandler == null ? new HttpClient(baseUrl) : new HttpClient(baseUrl, authorizationMessageHandler),
                "RestClient" => authorizationMessageHandler == null ? new RestClient(baseUrl) : new RestClient(baseUrl, authorizationMessageHandler),
                _ => throw new Exception($"Available api clients are '' or '' instead of '{clientName}'")
            };
        }
    }
}
