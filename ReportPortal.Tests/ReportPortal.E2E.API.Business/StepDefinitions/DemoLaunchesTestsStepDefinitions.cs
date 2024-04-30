using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.API.Business.Models.Responses;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.API.Business.StepDefinitions
{
    [Binding]
    public static class DemoLaunchesTestsStepDefinitions
    {
        [Given(@"New project '([^']*)' created with demo launches")]
        public static void GivenNewProjectCreatedWithDemoLaunches(string projectName)
        {
            var createProjectResponse = Steps.AsAdminUser().CreateProject<CreateProjectResponse>(projectName);
            CleanUpHelper.AddProjectId(createProjectResponse.ProjectId);
            Steps.AsAdminUser().CreateDemoData(projectName);
        }
    }
}
