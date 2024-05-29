using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core.Logger;

namespace ReportPortal.E2E.API.Business.Helpers
{
    public static class CleanUpHelper
    {
        private static readonly ILogger Log = TestsLogger.Create("CleanUpHelper");
        private static readonly List<int> ProjectIds = new();

        public static void AddProjectId(int id)
        {
            Log.LogInformation($"Added project Id '{id}' in cleanup list");
            ProjectIds.Add(id);
        }

        public static void CleanTestData()
        {
            Log.LogDebug($"Starting cleanup");
            foreach (var id in ProjectIds)
            {
                Log.LogDebug($"Deleting project id: {id}");
                Steps.AsAdminUser().DeleteProject(id);
            }
            Log.LogDebug($"Cleanup finished");
        }

        public static void CleanDemoData(string projectName)
        {
            var launchIds = Steps.AsAdminUser().GetLaunchesByFilter<GetLaunchesResponse>(projectName).Launches.Select(item => item.Id).ToList();
            launchIds.ForEach(id => Steps.AsAdminUser().DeleteLaunchById<SuccessfulMessageResponse>(projectName, id));
            var dashboardIds = Steps.AsAdminUser().GetDashboards<GetDashboardsResponse>(projectName).Dashboards.Select(item => item.Id).ToList();
            dashboardIds.ForEach(id => Steps.AsAdminUser().DeleteDashboard<SuccessfulMessageResponse>(projectName, id));
        }
    }
}
