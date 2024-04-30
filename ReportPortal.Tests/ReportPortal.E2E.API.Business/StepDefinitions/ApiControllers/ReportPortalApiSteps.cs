using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.HttpMessageHandlers;
using ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Models;
using HttpClient = ReportPortal.E2E.Core.HttpMessageHandlers.HttpClient.HttpClient;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        private readonly IHttpClient _launchApiSteps;
        private readonly string _baseUrl = TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().BaseUrl;
        private static readonly ILogger Log = TestsLogger.Create<ReportPortalApiSteps>();
        

        public ReportPortalApiSteps(AuthorizationMessageHandler authorizationMessageHandler)
        {
            //_launchApiSteps = new HttpClient(_baseUrl, authorizationMessageHandler);
            _launchApiSteps = new RestClient(_baseUrl, authorizationMessageHandler);
        }
    }
}
