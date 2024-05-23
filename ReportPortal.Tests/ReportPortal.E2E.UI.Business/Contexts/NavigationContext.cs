using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Business.Pages;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class NavigationContext : BaseContext
    {
        public NavigationContext(IWebDriver driver) : base(driver) { }

        private static readonly string BaseUrl =
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ReportPortalConfig>().BaseUrl;


        public void GoToReportPortalBaseUrl()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
        }

        public void GoToLaunchesPage(string projectName)
        {
            GoToPage(LaunchesPage.Url, projectName);
        }

        public void GoToDashboardsPage(string projectName)
        {
            GoToPage(DashboardsPage.Url, projectName);
        }


        private void GoToPage(string pageUrl, params string[] parameters)
        {
            var navigationUrl = $"{BaseUrl}{pageUrl.BindByPosition(parameters)}";
            if (navigationUrl.Equals(Driver.Url))
            {
                Driver.Navigate().Refresh();
                return;
            }
            Driver.Navigate().GoToUrl($"{BaseUrl}{pageUrl.BindByPosition(parameters)}");
        }
    }
}
