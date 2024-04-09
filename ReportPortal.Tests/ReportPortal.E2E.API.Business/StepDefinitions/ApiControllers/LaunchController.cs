using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
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
