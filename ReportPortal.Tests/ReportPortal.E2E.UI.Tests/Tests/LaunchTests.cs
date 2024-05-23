using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace ReportPortal.E2E.UI.Tests.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Edge")]
    public class LaunchTests : BaseTest
    {
        public LaunchTests(string browser) : base(browser) { }

        [SetUp]
        public void Background()
        {
            LoginContext.LoginAs(UserName);
        }

        [Test]
        public void Sorting_Launches_By_Start_Time()
        {
            NavigationContext.GoToLaunchesPage(Project);
            var launchesStartTime = LaunchesContext.WaitForReady().GetLaunchesStartTime();

            using (new AssertionScope())
            {
                launchesStartTime.Should().BeInDescendingOrder();
                LaunchesContext.SortByStartTime().GetLaunchesStartTime().Should().BeInAscendingOrder();
            }
        }

        [Test]
        public void Compare_Launches_Modal()
        {
            NavigationContext.GoToLaunchesPage(Project);
            var launches = LaunchesContext.WaitForReady().GetLaunches();
            LaunchesContext.ScrollIntoLaunch(launches.Last())
                .SelectLaunch(launches.Last())
                .SelectLaunch(launches[^2])
                .ClickCompareButton();

            using (new AssertionScope())
            {
                LaunchesContext.GetCompareModalTitle().Should().Be("COMPARE LAUNCHES");
                LaunchesContext.GetCompareModalSize().Item1.Should().BeGreaterThan(0);
                LaunchesContext.GetCompareModalSize().Item2.Should().BeGreaterThan(0);

                LaunchesContext.CloseCompareModal();
                LaunchesContext.IsModalDisappeared().Should().BeTrue();
            }
                
        }

        [Test]
        public void Delete_Launch()
        {
            NavigationContext.GoToLaunchesPage(Project);
            var launchToDelete = LaunchesContext.WaitForReady().GetLaunches().First();
            var launchFullName = launchToDelete.GetName;
            LaunchesContext.DeleteLaunchFromLaunchMenu(launchToDelete).ConfirmDeletion();

            LaunchesContext.GetLaunches().Select(launch => launch.GetName).Should().NotContain(launchFullName);
        }

        [Test]
        public void Resize_Dashboard()
        {
            NavigationContext.GoToDashboardsPage(Project);
            DashboardsContext.WaitForReady().GetDashboards().First().Click();
            var chartToResize = DashboardChartsContext.WaitForReady().GetCharts().First();
            var initialPosition = DashboardChartsContext.GetPosition(chartToResize);
            var resizedPosition = DashboardChartsContext.ResizeChart(chartToResize, -100, -100).GetPosition(chartToResize);

            using (new AssertionScope())
            {
                resizedPosition.X.Should().Be(initialPosition.X);
                resizedPosition.Y.Should().Be(initialPosition.Y);
                resizedPosition.Height.Should().BeLessThan(initialPosition.Height);
                resizedPosition.Width.Should().BeLessThan(initialPosition.Width);
            }
        }

        [Test]
        public void Move_Dashboard()
        {
            NavigationContext.GoToDashboardsPage(Project);
            DashboardsContext.WaitForReady().GetDashboards().First().Click();
            var chartToMove = DashboardChartsContext.WaitForReady().GetCharts()[1];
            var initialPosition = DashboardChartsContext.GetPosition(chartToMove);
            var movedPosition = DashboardChartsContext
                .MoveChart(chartToMove, initialPosition.Width, initialPosition.Height)
                .GetPosition(chartToMove);

            using (new AssertionScope())
            {
                movedPosition.X.Should().BeGreaterThan(initialPosition.X);
                movedPosition.Y.Should().BeGreaterThan(initialPosition.Y);
                movedPosition.Height.Should().Be(initialPosition.Height);
                movedPosition.Width.Should().Be(initialPosition.Width);
            }
        }

        [Test]
        [Ignore("Fail test for screenshot")]
        public void TEST_TO_FAIL_AND_GET_SCREENSHOT()
        {
            NavigationContext.GoToLaunchesPage(Project);
            var launches = LaunchesContext.WaitForReady().GetLaunches();
            LaunchesContext.ScrollIntoLaunch(launches.Last())
                .SelectLaunch(launches.Last())
                .SelectLaunch(launches[^2])
                .ClickCompareButton();

            using (new AssertionScope())
            {
                LaunchesContext.GetCompareModalTitle().Should().Be("COMPARE LAUNCHES");
                LaunchesContext.GetCompareModalSize().Item1.Should().BeGreaterThan(0);
                LaunchesContext.GetCompareModalSize().Item2.Should().BeGreaterThan(0);
                LaunchesContext.IsModalDisappeared().Should().BeTrue();
            }

        }
    }
}
