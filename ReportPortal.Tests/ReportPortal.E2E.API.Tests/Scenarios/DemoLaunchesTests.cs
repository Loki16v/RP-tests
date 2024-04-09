using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.StepDefinitions;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Helpers;

namespace ReportPortal.E2E.API.Tests.Scenarios
{
    public class DemoLaunchesTests : BaseNunitTest
    {
        private static readonly string ProjectName = RandomValuesHelper.RandomString();

        protected override void Preconditions()
        {
            DemoLaunchesTestsStepDefinitions.GivenNewProjectCreatedWithDemoLaunches(ProjectName);
        }

        //Dummy test
        [Test]
        public async Task Launch_Demo_Name()
        {
            var getLaunchesNamesResponse = await Steps.AsAdminUser().GetLaunchNames(ProjectName);
            getLaunchesNamesResponse.EnsureSuccessStatusCode();
            var names = getLaunchesNamesResponse.GetResponse<List<string>>();

            names.Should().HaveCount(1);
            names!.Single().Should().Be("Demo Api Tests");
        }
    }
}
