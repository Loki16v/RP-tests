using ReportPortal.E2E.API.Core.Business.Models.Responses;
using ReportPortal.E2E.Common.Extensions;
using ReportPortal.E2E.UI.Core.Business.Helpers;
using TechTalk.SpecFlow;
using Steps = ReportPortal.E2E.API.Core.Business.Steps;

namespace ReportPortal.E2E.UI.Core.Business.StepDefinitions.API
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
