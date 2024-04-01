using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Core.Business.Models;

namespace ReportPortal.E2E.API.Core.Business.StepDefinitions
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
            Log.LogInformation($"CreateProject '{projectName}' with EntryType '{entryType}'\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.PostAsJsonAsync(endpoint, requestBody);
        }

        public void DeleteProject(int id)
        {
            var endpoint = $"{Endpoints.ManageProject}/{id}";
            Log.LogInformation($"DeleteProject with Id '{id}'\n Method: Delete\n Endpoint: {endpoint}");
            _launchApiSteps.DeleteAsync(endpoint);
        }

    }
}
