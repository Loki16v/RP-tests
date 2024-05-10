using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient
{
    public class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public HttpClient(string url)
        {
            _httpClient = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri(url)
            };
        }

        public HttpClient(string url, HttpMessageHandler httpMessageHandler)
        {
            _httpClient = new System.Net.Http.HttpClient(httpMessageHandler)
            {
                BaseAddress = new Uri(url)
            };
        }

        public TokenInformation GetToken(List<KeyValuePair<string, string>> body)
        {
            var requestUri = TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().ApiAuthUrl;
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new FormUrlEncodedContent(body)
            };
            httpRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("ui:uiman")));
            return _httpClient.Send(httpRequest).GetResponse<TokenInformation>();
        }

        public T Get<T>(string requestUri)
        {
            return _httpClient.GetAsync(requestUri).Result.GetResponse<T>();
        }

        public T Post<T>(string requestUri, object body)
        {
            return _httpClient.PostAsJsonAsync(requestUri, body).Result.GetResponse<T>();
        }

        public void Post(string requestUri, object body)
        {
            _httpClient.PostAsJsonAsync(requestUri, body).GetAwaiter().GetResult();
        }

        public T Put<T>(string requestUri, object body)
        {
            return _httpClient.PutAsJsonAsync(requestUri, body).Result.GetResponse<T>();
        }

        public T Delete<T>(string requestUri)
        {
            return _httpClient.DeleteAsync(requestUri).Result.GetResponse<T>();
        }

        public void Delete(string requestUri)
        {
            _httpClient.DeleteAsync(requestUri);
        }
    }
}
