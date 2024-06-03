using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;
using ReportPortal.E2E.Core.Enums;
using ReportPortal.E2E.Core.Extensions;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public T CreateProject<T>(string projectName, EntryType entryType = EntryType.Internal)
        {
            var requestBody = new
            {
                entryType = entryType.GetName(),
                projectName = projectName
            };
            var endpoint = Endpoints.ManageProject;
            Log.LogInformation("CreateProject '{ProjectName}' with EntryType '{EntryType}'\n Method: Post\n Endpoint: {Endpoint}",
                projectName, entryType, endpoint);
            return _launchApiSteps.Post<T>(endpoint, requestBody);
        }

        public void DeleteProject(int id)
        {
            var endpoint = $"{Endpoints.ManageProject}/{id}";
            Log.LogInformation("DeleteProject with Id '{Id}'\n Method: Delete\n Endpoint: {Endpoint}", id, endpoint);
            _launchApiSteps.Delete(endpoint);
        }

        public T GetProjectsList<T>()
        {
            var endpoint = $"{Endpoints.GetProjectList}";
            Log.LogInformation("GetProjectsList \n Method: Get\n Endpoint: {Endpoint}", endpoint);
            return _launchApiSteps.Get<T>(endpoint);
        }

        public T SearchProjectUser<T>(string projectName, string userName)
        {
            var endpoint = string.Format(Endpoints.SearchProjectUser, projectName, userName);
            Log.LogInformation("SearchProjectUser \n Method: Get\n Endpoint: {Endpoint}", endpoint);
            return _launchApiSteps.Get<T>(endpoint);
        }
      
        public T AddUserToProject<T>(string projectName, string userName, ProjectRole userRole)
        {
            var body = new
            {
                userNames = new[]{ new Dictionary<string, string>{ {userName, userRole.GetName()} }}
            };
            var endpoint = string.Format(Endpoints.AddUserToProject, projectName);
            Log.LogInformation("AddUserToProject user name:{UserName} \n Method: Put\n Endpoint: {Endpoint}", userName, endpoint);
            return _launchApiSteps.Put<T>(endpoint, body);
        }
    }
}
