using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.Common.Models;

namespace ReportPortal.E2E.Common.HttpMessageHandlers
{
    public class ClientsHandler
    {
        private static Dictionary<string, AuthorizationMessageHandler> _authenticationHandlers;
        private readonly HttpClient _httpClient;

        public ClientsHandler()
        {
            _authenticationHandlers = new Dictionary<string, AuthorizationMessageHandler>();
            _httpClient = new HttpClient();
        }

        public async Task<HttpMessageHandler> GetAuthHttpMessageHandler(UserCredentials userCredentials, bool? refreshAuth = false)
        {
            if (!_authenticationHandlers.ContainsKey(userCredentials.UserName) || refreshAuth.GetValueOrDefault())
            {
                await CreateAuthToken(userCredentials);
            }

            else if (IsTokenExpired(userCredentials))
            {
                _authenticationHandlers.Remove(userCredentials.UserName);
                await CreateAuthToken(userCredentials);
            }

            return _authenticationHandlers[userCredentials.UserName];
        }

        public async Task<TokenInformation> CreateAuthToken(UserCredentials userCredentials)
        {
            var requestUri = TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().ApiAuthUrl;
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new("grant_type", "password"),
                    new("username", userCredentials.UserName),
                    new("password", userCredentials.Password)
                })
            };
            httpRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("ui:uiman")));
            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var tokenInfo = await response.Content.ReadFromJsonAsync<TokenInformation>();
            _authenticationHandlers[userCredentials.UserName] = new AuthorizationMessageHandler(tokenInfo, () => CreateAuthToken(userCredentials))
                { InnerHandler = new HttpClientHandler() };
            return tokenInfo;
        }

        private static bool IsTokenExpired(UserCredentials userCredentials)
        {
            var tokenInfo = _authenticationHandlers[userCredentials.UserName];
            return tokenInfo.ExpiredDate <= DateTime.UtcNow;
        }
    }
}
