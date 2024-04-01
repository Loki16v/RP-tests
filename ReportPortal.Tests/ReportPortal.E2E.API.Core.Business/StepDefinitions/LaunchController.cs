using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Core.Business.Models;

namespace ReportPortal.E2E.API.Core.Business.StepDefinitions
{
    public partial class ReportPortalApiSteps
    {
        public Task<HttpResponseMessage> GetLaunchNames(string projectName)
        {
            var endpoint = string.Format(Endpoints.GetLaunchNames, projectName);
            Log.LogInformation($"GetLaunchNames for project '{projectName}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.GetAsync(endpoint);
        }
    }
}
