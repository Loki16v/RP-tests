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
            if (!Enum.TryParse(clientName, out Client apiClient))
                throw new NotSupportedException($"Available api clients are '{Client.RestClient}' or '{Client.HttpClient}' instead of '{clientName}'");

            return apiClient switch
            {
                Client.HttpClient => authorizationMessageHandler == null ? new HttpClient(baseUrl) : new HttpClient(baseUrl, authorizationMessageHandler),
                Client.RestClient => authorizationMessageHandler == null ? new RestClient(baseUrl) : new RestClient(baseUrl, authorizationMessageHandler),
                _ => throw new NotSupportedException($"Unsupported http client type '{apiClient}'")
            };
        }

        private enum Client
        {
            None = 0,
            HttpClient,
            RestClient
        }
    }
}
