using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Core.Business;
using ReportPortal.E2E.API.Core.Business.Models.Responses;
using ReportPortal.E2E.Common.Extensions;
using ReportPortal.E2E.Common.Helpers;

namespace ReportPortal.E2E.API.Tests.Scenarios
{
    public class DemoLaunchesTests : BaseTest
    {
        private static readonly string ProjectName = RandomValuesHelper.RandomString();
        private int _projectId;

        protected override void Preconditions()
        {
            var createProjectResponse = Steps.AsAdminUser().CreateProject(ProjectName).GetAwaiter().GetResult();
            createProjectResponse.EnsureSuccessStatusCode();
            _projectId = createProjectResponse.GetResponse<CreateProjectResponse>().ProjectId;

            var generateDataResponse = Steps.AsAdminUser().CreateDemoData(ProjectName).GetAwaiter().GetResult();
            generateDataResponse.EnsureSuccessStatusCode();
        }

        //Dummy test
        [Test]
        public async Task Launch_Demo_Name()
        {
            var getLaunchesNamesResponse = await Steps.AsAdminUser().GetLaunchNames(ProjectName);
            getLaunchesNamesResponse.EnsureSuccessStatusCode();
            var names = getLaunchesNamesResponse.GetResponse<List<string>>();

            names.Should().HaveCount(1);
            names!.First().Should().Be("Demo Api Tests");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Steps.AsAdminUser().DeleteProject(_projectId);
        }

    }
}
