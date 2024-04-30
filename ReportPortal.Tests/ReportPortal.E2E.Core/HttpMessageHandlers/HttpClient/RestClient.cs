using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.Core.Models;
using RestSharp;

namespace ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient
{
    public class RestClient : IHttpClient
    {
        private readonly RestSharp.RestClient _restClient;

        public RestClient(string url)
        {
            _restClient = new RestSharp.RestClient(url);
        }

        public RestClient(string url, AuthorizationMessageHandler authorizationMessageHandler)
        {
            _restClient = new RestSharp.RestClient(url);
            _restClient.AddDefaultHeader("Authorization", authorizationMessageHandler.GetTokenValue());
        }

        public TokenInformation GetToken(List<KeyValuePair<string, string>> body)
        {
            var requestUri = TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().ApiAuthUrl;
            var httpRequest = new RestRequest(requestUri, Method.Post);
            body.ForEach(x => httpRequest.AddParameter(x.Key, x.Value));
            httpRequest.AddHeader("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("ui:uiman")));
            return _restClient.ExecuteAsync<TokenInformation>(httpRequest).Result.Data;
        }

        public T Get<T>(string requestUri)
        {
            return _restClient.ExecuteAsync<T>(new RestRequest(requestUri)).Result.Data;
        }

        public T Post<T>(string requestUri, object body)
        {
            return _restClient.ExecuteAsync<T>(new RestRequest(requestUri, Method.Post).AddJsonBody(body)).Result.Data;
        }

        public void Post(string requestUri, object body)
        {
            _restClient.ExecuteAsync(new RestRequest(requestUri, Method.Post).AddJsonBody(body)).GetAwaiter().GetResult();
        }

        public T Put<T>(string requestUri, object body)
        {
            return _restClient.ExecuteAsync<T>(new RestRequest(requestUri, Method.Put).AddJsonBody(body)).Result.Data;
        }

        public T Delete<T>(string requestUri)
        {
            return _restClient.ExecuteAsync<T>(new RestRequest(requestUri), Method.Delete).Result.Data;
        }

        public void Delete(string requestUri)
        {
            _restClient.ExecuteAsync(new RestRequest(requestUri));
        }
    }
}
