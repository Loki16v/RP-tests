using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Business.Models.Responses.Items;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Tests.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Edge")]
    public class LaunchViewTests : BaseTest
    {
        public LaunchViewTests(string browser) : base(browser) { }

        private LaunchItem _testLaunchData;
        private LaunchContainer _testLaunch;

        [OneTimeSetUp]
        public void BackGround()
        {
            _testLaunchData = Steps.AsAdminUser().GetLaunchesByFilter<GetLaunchesResponse>(Project).Launches
                .First(item => item.Statistics.Executions.Passed != 0 &&
                               item.Statistics.Executions.Failed != 0 &&
                               item.Statistics.Executions.Skipped != 0 &&
                               item.Statistics.Defects.AutomationBug.Total != 0 &&
                               item.Statistics.Defects.ProductBug.Total != 0 &&
                               item.Statistics.Defects.SystemIssue.Total != 0 &&
                               item.Statistics.Defects.ToInvestigate.Total != 0);
        }

        [SetUp]
        public void BackgroundSteps()
        {
            LoginContext.LoginAs(UserName);
            NavigationContext.GoToLaunchesPage(Project);
            _testLaunch = LaunchesContext.GetLaunches().Single(item => item.GetName.EndsWith($"#{_testLaunchData.Number}"));
        }



        [Test]
        public void Move_To_Launch_Details_View_By_Name()
        {
            var launchRunDetails = LaunchesContext.NavigateToLaunchDetailsByNameLink(_testLaunch).GetLaunchRunDetails();

            launchRunDetails.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Move_To_Launch_All_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchAllTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Executions.Total);
        }

        [Test]
        public void Move_To_Launch_Passed_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchPassedTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Executions.Passed);
        }

        [Test]
        public void Move_To_Launch_Failed_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchFailedTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Executions.Failed);
        }

        [Test]
        public void Move_To_Launch_Skipped_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchSkippedTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Executions.Skipped);
        }

        [Test]
        public void Move_To_Launch_Product_Bug_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchProductBugTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Defects.ProductBug.Total);
        }

        [Test]
        public void Move_To_Launch_Automation_Bug_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchAutomationBugTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Defects.AutomationBug.Total);
        }

        [Test]
        public void Move_To_Launch_System_Issue_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchSystemIssueTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Defects.SystemIssue.Total);
        }

        [Test]
        public void Move_To_Launch_To_Investigate_Tests()
        {
            var testsInfo = LaunchesContext.NavigateToLaunchToInvestigateTestsView(_testLaunch).GetTestsInfo();

            testsInfo.Count.Should().Be(_testLaunchData.Statistics.Defects.ToInvestigate.Total);
        }
    }
}
