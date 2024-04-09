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

        public Task<HttpResponseMessage> GetLaunchesByFilter(string projectName, string query)
        {
            var endpoint = string.Format(Endpoints.GetLaunchesByFilter, projectName) + query;
            Log.LogInformation($"GetLaunchesByFilter for project '{projectName}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.GetAsync(endpoint);
        }

        public Task<HttpResponseMessage> GetLatestLaunchByFilter(string projectName, string query)
        {
            var endpoint = string.Format(Endpoints.GetLatestLaunchByFilter, projectName) + query;
            Log.LogInformation(
                $"GetLatestLaunchByFilter for project '{projectName}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.GetAsync(endpoint);
        }
    }
}
