using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.Pages;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class DashboardsContext : BaseContext
    {
        public DashboardsContext(IWebDriver driver) : base(driver) { }

        private DashboardsPage DashboardsPage => new(Driver);

        public List<IWebElement> GetDashboards()
        {
            return DashboardsPage.Dashboards.ToList();
        }

        public DashboardsContext WaitForReady()
        {
            DashboardsPage.WaitForReady();
            return this;
        }
    }
}
