using System.Net.Http.Headers;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.Core.HttpMessageHandlers
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private TokenInformation Token { get; set; }
        private Func<TokenInformation> AuthTokenGetter { get; }
        public DateTime ExpiredDate { get; private set; }

        internal AuthorizationMessageHandler(TokenInformation tokenInfo, Func<TokenInformation> authTokenGetter)
        {
            Token = tokenInfo;
            ExpiredDate = tokenInfo.TokenTimeReceiving.AddSeconds(tokenInfo.ExpiresIn);
            AuthTokenGetter = authTokenGetter;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RenewTokenIfExpired();
            request.Headers.Authorization = new AuthenticationHeaderValue(Token.TokenType, Token.AccessToken);
            return await base.SendAsync(request, cancellationToken);
        }

        private void RenewTokenIfExpired()
        {
            if (ExpiredDate >= DateTime.UtcNow) return;

            Token = AuthTokenGetter();
            ExpiredDate = DateTime.UtcNow.AddSeconds(Token.ExpiresIn);
        }

        public string GetTokenValue()
        {
            RenewTokenIfExpired();
            return $"{Token.TokenType} {Token.AccessToken}";
        }
    }
}
