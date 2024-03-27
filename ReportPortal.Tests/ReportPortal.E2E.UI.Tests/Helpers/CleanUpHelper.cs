using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Framework;
using ReportPortal.E2E.Core.Logger;

namespace ReportPortal.E2E.UI.Tests.Helpers
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
            Log.LogInformation($"Starting cleanup");
            foreach (var id in ProjectIds)
            {
                Steps.AsAdminUser().DeleteProject(id);
            }
            Log.LogInformation($"Cleanup finished");
        }
    }
}
