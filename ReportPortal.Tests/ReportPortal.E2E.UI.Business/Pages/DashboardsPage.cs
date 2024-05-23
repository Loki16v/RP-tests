using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class DashboardsPage : BasePage
    {
        public const string Url = "/ui/#{projectName}/dashboard";

        private const string DashboardsLocator = "//*[contains(@class,'dashboardTable__dashboard-table')]//a";

        public DashboardsPage(IWebDriver driver) : base(driver) { }

        public IReadOnlyCollection<IWebElement> Dashboards => Driver.FindElements(By.XPath(DashboardsLocator));

        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => Dashboards.Count != 0);
        }
    }
}
