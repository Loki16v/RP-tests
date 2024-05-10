using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.Core.HttpMessageHandlers
{
    public class ClientsHandler
    {
        private static ConcurrentDictionary<string, AuthorizationMessageHandler> _authenticationHandlers;
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl = TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().BaseUrl;

        public ClientsHandler()
        {
            _authenticationHandlers = new ConcurrentDictionary<string, AuthorizationMessageHandler>();
            _httpClient = ApiClient.Get(_baseUrl);
        }

        public AuthorizationMessageHandler GetAuthHttpMessageHandler(UserCredentials userCredentials, bool? refreshAuth = false)
        {
            if (!_authenticationHandlers.ContainsKey(userCredentials.UserName) || refreshAuth.GetValueOrDefault())
            {
                CreateAuthToken(userCredentials);
            }

            else if (IsTokenExpired(userCredentials))
            {
                _authenticationHandlers.Remove(userCredentials.UserName, out _);
                CreateAuthToken(userCredentials);
            }

            return _authenticationHandlers[userCredentials.UserName];
        }

        public TokenInformation CreateAuthToken(UserCredentials userCredentials)
        {
            var body = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "password"),
                new("username", userCredentials.UserName),
                new("password", userCredentials.Password)
            };
            var tokenInfo = _httpClient.GetToken(body);
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
