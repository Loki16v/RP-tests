using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core.Extensions;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.API.Business.StepDefinitions
{
    [Binding]
    public class DemoLaunchesTestsStepDefinitions
    {
        [Given(@"New project '([^']*)' created with demo launches")]
        public void GivenNewProjectCreatedWithDemoLaunches(string projectName)
        {
            var response = Steps.AsAdminUser().CreateProject(projectName).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            CleanUpHelper.AddProjectId(response.GetResponse<CreateProjectResponse>().ProjectId);
            Steps.AsAdminUser().CreateDemoData(projectName).GetAwaiter().GetResult();
        }
    }
}
