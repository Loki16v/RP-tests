using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        private readonly HttpClient _launchApiSteps;
        private readonly string _baseUrl = TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().BaseUrl;
        private static readonly ILogger Log = TestsLogger.Create<ReportPortalApiSteps>();
        

        public ReportPortalApiSteps(HttpMessageHandler httpMessageHandler)
        {
            _launchApiSteps = new HttpClient(httpMessageHandler){BaseAddress = new Uri(_baseUrl) };
        }
    }
}
