using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class LaunchDetailsPage : BasePage
    {
        public LaunchDetailsPage(IWebDriver driver) : base(driver) { }

        private const string LaunchContainerLocator = "//*[contains(@class,'grid__grid')]/*[@data-id]";

        internal List<LaunchDetailContainer> LaunchDetailsList =>
            new(Driver.FindElements(By.XPath(LaunchContainerLocator))
                .Select(x => new LaunchDetailContainer(x)));

        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => !Driver.ElementExistsByXPath(SpinnerLocator));
        }
    }
}
