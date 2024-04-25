using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;

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

        public void GoTo(string urlTemplate, params string[] parameters)
        {
            Driver.Navigate().GoToUrl($"{BaseUrl}{urlTemplate.BindByPosition(parameters)}");
        }

    }
}
