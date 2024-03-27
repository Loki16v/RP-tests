using ReportPortal.E2E.API.Framework.Extensions;
using ReportPortal.E2E.API.Framework.Models.Responses;
using ReportPortal.E2E.UI.Tests.Helpers;
using TechTalk.SpecFlow;
using Steps = ReportPortal.E2E.API.Framework.Steps;

namespace ReportPortal.E2E.UI.Tests.StepDefinitions.API
{
    [Binding]
    public class DemoLaunchesTestsStepDefinitions
    {
        [Given(@"New project '([^']*)' created with demo launches")]
        public void GivenNewProjectCreatedWithDemoLaunches(string projectName)
        {
            var response = Steps.AsAdminUser().CreateProject(projectName).GetAwaiter().GetResult();
            CleanUpHelper.AddProjectId(response.GetResponse<CreateProjectResponse>().ProjectId);
            Steps.AsAdminUser().CreateDemoData(projectName).GetAwaiter().GetResult();
        }
    }
}
