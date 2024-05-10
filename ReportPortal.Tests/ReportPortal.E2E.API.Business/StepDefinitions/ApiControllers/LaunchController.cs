using Microsoft.Extensions.Logging;
using ReportPortal.Client.Abstractions.Requests;
using ReportPortal.E2E.API.Business.Models;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public T GetLaunchNames<T>(string projectName)
        {
            var endpoint = string.Format(Endpoints.GetLaunchNames, projectName);
            Log.LogInformation($"GetLaunchNames for project '{projectName}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.Get<T>(endpoint);
        }
      
        public T GetLaunchesByFilter<T>(string projectName, string query = null)
        {
            var endpoint = string.Format(Endpoints.GetLaunchesByFilter, projectName) + query ?? "";
            Log.LogInformation($"GetLaunchesByFilter for project '{projectName}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.Get<T>(endpoint);
        }

        public T GetLatestLaunchByFilter<T>(string projectName, string query)
        {
            var endpoint = string.Format(Endpoints.GetLatestLaunchByFilter, projectName) + query;
            Log.LogInformation($"GetLatestLaunchByFilter for project '{projectName}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.Get<T>(endpoint);
        }

        public T GetLaunchById<T>(string projectName, int id)
        {
            var endpoint = string.Format(Endpoints.GetLaunchById, projectName, id);
            Log.LogInformation($"GetLaunchById for project '{projectName}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.Get<T>(endpoint);
        }

        public T PostLaunchCluster<T>(string projectName, string launchId, bool removeNumbers = true)
        {
            var body = new
            {
                launchId = launchId,
                removeNumbers = removeNumbers
            };
            var endpoint = string.Format(Endpoints.PostCluster, projectName);
            Log.LogInformation($"PostLaunchCluster for project '{projectName}'\n Method: Post\n Endpoint: {endpoint}");
            return _launchApiSteps.Post<T>(endpoint, body);
        }

        public T PutLaunchUpdate<T>(string projectName, int launchId, UpdateLaunchRequest body)
        {
            var endpoint = string.Format(Endpoints.LaunchUpdate, projectName, launchId);
            Log.LogInformation($"PutLaunchUpdate for project '{projectName}'\n Method: Put\n Endpoint: {endpoint}");
            return _launchApiSteps.Put<T>(endpoint, body);
        }

        public T DeleteLaunchById<T>(string projectName, int launchId)
        {
            var endpoint = string.Format(Endpoints.DeleteLaunchById, projectName, launchId);
            Log.LogInformation($"DeleteLaunchById for project '{projectName}'\n Method: Delete\n Endpoint: {endpoint}");
            return _launchApiSteps.Delete<T>(endpoint);
        }
    }
}
