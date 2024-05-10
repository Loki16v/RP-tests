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
            return _restClient.Execute<TokenInformation>(httpRequest).Data;
        }

        public T Get<T>(string requestUri)
        {
            return _restClient.Execute<T>(new RestRequest(requestUri)).Data;
        }

        public T Post<T>(string requestUri, object body)
        {
            return _restClient.Execute<T>(new RestRequest(requestUri, Method.Post).AddJsonBody(body)).Data;
        }

        public void Post(string requestUri, object body)
        {
            _restClient.Execute(new RestRequest(requestUri, Method.Post).AddJsonBody(body));
        }

        public T Put<T>(string requestUri, object body)
        {
            return _restClient.Execute<T>(new RestRequest(requestUri, Method.Put).AddJsonBody(body)).Data;
        }

        public T Delete<T>(string requestUri)
        {
            return _restClient.Execute<T>(new RestRequest(requestUri), Method.Delete).Data;
        }

        public void Delete(string requestUri)
        {
            _restClient.ExecuteAsync(new RestRequest(requestUri));
        }
    }
}
