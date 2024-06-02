using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class LaunchTestsInfoPage : BasePage
    {
        public LaunchTestsInfoPage(IWebDriver driver) : base(driver) { }

        private const string TestInfoContainerLocator = "//*[contains(@class,'gridRow__grid-row-wrapper')]";

        internal List<TestInfoContainer> TestsInfoList =>
            new(Driver.FindElements(By.XPath(TestInfoContainerLocator))
                .Select(x => new TestInfoContainer(x)));

        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => !Driver.ElementExistsByXPath(SpinnerLocator));
        }
    }
}
