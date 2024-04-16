using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core.Extensions;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.API.Business.StepDefinitions
{
    [Binding]
    public static class DemoLaunchesTestsStepDefinitions
    {
        [Given(@"New project '([^']*)' created with demo launches")]
        public static void GivenNewProjectCreatedWithDemoLaunches(string projectName)
        {
            var createProjectResponse = Steps.AsAdminUser().CreateProject(projectName).GetAwaiter().GetResult();
            createProjectResponse.EnsureSuccessStatusCode();
            CleanUpHelper.AddProjectId(createProjectResponse.GetResponse<CreateProjectResponse>().ProjectId);

            var generateDataResponse = Steps.AsAdminUser().CreateDemoData(projectName).GetAwaiter().GetResult();
            generateDataResponse.EnsureSuccessStatusCode();
        }
    }
}
