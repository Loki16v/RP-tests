using System.Net.Http.Headers;
using ReportPortal.E2E.Common.Models;

namespace ReportPortal.E2E.Common.HttpMessageHandlers
{
    internal class AuthorizationMessageHandler : DelegatingHandler
    {
        public TokenInformation Token { get; set; }
        private Func<Task<TokenInformation>> AuthTokenGetter { get; }
        public DateTime ExpiredDate { get; private set; }

        internal AuthorizationMessageHandler(TokenInformation tokenInfo, Func<Task<TokenInformation>> authTokenGetter)
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

            Token = AuthTokenGetter().GetAwaiter().GetResult();
            ExpiredDate = DateTime.UtcNow.AddSeconds(Token.ExpiresIn);
        }
    }
}
