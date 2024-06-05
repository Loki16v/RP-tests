using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public T GetDashboards<T>(string projectName)
        {
            var endpoint = string.Format(Endpoints.GetDashboards, projectName);
            Log.LogInformation("GetDashboards for project '{ProjectName}'\n Method: Get\n Endpoint: {Endpoint}", projectName, endpoint);
            return _launchApiSteps.Get<T>(endpoint);
        }

        public T DeleteDashboard<T>(string projectName, int id)
        {
            var endpoint = string.Format(Endpoints.DeleteDashboard, projectName, id);
            Log.LogInformation("DeleteDashboard from project '{ProjectName}'\n Method: Delete\n Endpoint: {Endpoint}", projectName, endpoint);
            return _launchApiSteps.Delete<T>(endpoint);
        }
    }
}
