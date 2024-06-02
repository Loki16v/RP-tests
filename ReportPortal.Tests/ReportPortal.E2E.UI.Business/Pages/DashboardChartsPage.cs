using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class DashboardChartsPage : BasePage
    {
        public DashboardChartsPage(IWebDriver driver) : base(driver) { }

        private const string ChartLocator =
            "//*[contains(@class,'react-grid-item')][contains(@class,'widgetsGrid__widget-view')]";

        internal List<ChartContainer> Charts => new(Driver.FindElements(By.XPath(ChartLocator))
            .Select(x => new ChartContainer(x)));

        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => Charts.Count !=0 && !Driver.ElementExistsByXPath(SpinnerLocator));
        }
    }
}
