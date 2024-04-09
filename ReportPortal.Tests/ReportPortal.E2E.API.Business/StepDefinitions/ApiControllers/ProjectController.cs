using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public Task<HttpResponseMessage> CreateProject(string projectName, string entryType = "INTERNAL")
        {
            var requestBody = new
            {
                entryType = entryType,
                projectName = projectName
            };
            var endpoint = Endpoints.ManageProject;
            Log.LogInformation($"CreateProject '{projectName}' with EntryType '{entryType}'\n Method: Post\n Endpoint: {endpoint}");
            return _launchApiSteps.PostAsJsonAsync(endpoint, requestBody);
        }

        public void DeleteProject(int id)
        {
            var endpoint = $"{Endpoints.ManageProject}/{id}";
            Log.LogInformation($"DeleteProject with Id '{id}'\n Method: Delete\n Endpoint: {endpoint}");
            _launchApiSteps.DeleteAsync(endpoint);
        }

        public Task<HttpResponseMessage> GetProjectsList()
        {
            var endpoint = $"{Endpoints.GetProjectList}";
            Log.LogInformation($"GetProjectsList \n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.GetAsync(endpoint);
        }

        public Task<HttpResponseMessage> SearchProjectUser(string projectName, string userName)
        {
            var endpoint = string.Format(Endpoints.SearchProjectUser, projectName, userName);
            Log.LogInformation($"SearchProjectUser \n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.GetAsync(endpoint);
        }

    }
}
