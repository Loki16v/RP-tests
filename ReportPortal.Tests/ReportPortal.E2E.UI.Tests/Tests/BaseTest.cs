using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Driver;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Business.Contexts;

namespace ReportPortal.E2E.UI.Tests.Tests
{
    public class BaseTest
    {
        protected readonly string UserName =
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<UserCredentials>().UserName;

        protected readonly string Project =
            TestsBootstrap.Instance.Configuration.GetSection("PersonalProject").GetValueOrThrow();

        private readonly bool _isRemote =
            bool.TryParse(TestsBootstrap.Instance.Configuration.GetSection("RemoteRun").Value, out var _);

        private DriverFactory _driverFactory;
        protected string Browser;
        protected IWebDriver Driver;
        protected LoginContext LoginContext;
        protected LaunchesContext LaunchesContext;
        protected DashboardsContext DashboardsContext;
        protected DashboardChartsContext DashboardChartsContext;
        protected NavigationContext NavigationContext;

        public BaseTest(string browser)
        {
            Browser = browser;
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Steps.AsAdminUser().CreateDemoData(Project);
        }

        [SetUp]
        public void Setup()
        {
            _driverFactory = new DriverFactory(Browser, _isRemote, TestContext.CurrentContext.Test.Name);
            Driver = _driverFactory.GetDriver();

            LoginContext = new LoginContext(Driver);
            LaunchesContext = new LaunchesContext(Driver);
            DashboardsContext = new DashboardsContext(Driver);
            DashboardChartsContext = new DashboardChartsContext(Driver);
            NavigationContext = new NavigationContext(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            var testResult = TestContext.CurrentContext.Result.Outcome.Status;

            if (testResult == TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var screenshotPath = Path.Combine(AppContext.BaseDirectory, "TestResults");
                if (!Directory.Exists(screenshotPath)) Directory.CreateDirectory(screenshotPath);
                screenshot.SaveAsFile(Path.Combine(screenshotPath,
                    $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yy_MM_dd__HH_mm_ss}.png"));
            }

            if (_isRemote) UpdateRemoteTestResult(testResult);
            
            _driverFactory.CloseDriver();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            CleanUpHelper.CleanDemoData(Project);
        }


        private void UpdateRemoteTestResult(TestStatus status)
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("lambda-status=" + (status == TestStatus.Passed ? "passed" : "failed"));
            }
            catch
            {
                // ignored
            }
        }
    }
}
