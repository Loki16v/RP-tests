using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.CustomElements;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class DashboardsPage : BasePage
    {
        public const string Url = "/ui/#{projectName}/dashboard";

        private const string DashboardsLocator = "//*[contains(@class,'dashboardTable__dashboard-table')]//a";

        public DashboardsPage(IWebDriver driver) : base(driver) { }

        public List<Button> Dashboards => new(Driver.FindElements(By.XPath(DashboardsLocator)).Select(x => new Button(x)));

        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => Dashboards.Count != 0);
        }
    }
}
