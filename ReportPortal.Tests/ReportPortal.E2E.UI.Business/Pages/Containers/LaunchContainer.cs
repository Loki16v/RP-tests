using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Pages.Containers
{
    public class LaunchContainer
    {
        private readonly IWebElement _webElement;

        public LaunchContainer(IWebElement webElement)
        {
            _webElement = webElement;
        }

        private const string TotalCountLocator = ".//*[contains(@class,'launchSuiteGrid__total-col')]//a";
        private const string PassedCountLocator = ".//*[contains(@class,'launchSuiteGrid__passed-col')]//a";
        private const string FailedCountLocator = ".//*[contains(@class,'launchSuiteGrid__failed-col')]//a";
        private const string SkippedCountLocator = ".//*[contains(@class,'launchSuiteGrid__skipped-col')]//a";


        internal IWebElement TotalCount =>
            _webElement.FindElement(By.XPath(TotalCountLocator));
        internal IWebElement PassedCount =>
            _webElement.FindElement(By.XPath(PassedCountLocator));
        internal IWebElement FailedCount =>
            _webElement.FindElement(By.XPath(FailedCountLocator));
        internal IWebElement SkippedCount =>
            _webElement.FindElement(By.XPath(SkippedCountLocator));
    }
}
