using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient
{
    public interface IHttpClient
    {
        public TokenInformation GetToken(List<KeyValuePair<string, string>> body);

        public T Get<T>(string requestUri);

        public T Post<T>(string requestUri, object body);

        public void Post(string requestUri, object body);

        public T Put<T>(string requestUri, object body);

        public void Put(string url, object body, List<KeyValuePair<string, string>> headers);

        public T Delete<T>(string requestUri);

        public void Delete(string requestUri);
    }
}
