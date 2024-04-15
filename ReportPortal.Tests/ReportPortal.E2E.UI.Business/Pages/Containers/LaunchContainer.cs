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

        internal IWebElement TotalCount =>
            _webElement.FindElement(By.XPath(".//div[contains(@class,'launchSuiteGrid__total-col')]//a"));
        internal IWebElement PassedCount =>
            _webElement.FindElement(By.XPath(".//div[contains(@class,'launchSuiteGrid__passed-col')]//a"));
        internal IWebElement FailedCount =>
            _webElement.FindElement(By.XPath(".//div[contains(@class,'launchSuiteGrid__failed-col')]//a"));
        internal IWebElement SkippedCount =>
            _webElement.FindElement(By.XPath(".//div[contains(@class,'launchSuiteGrid__skipped-col')]//a"));
    }
}
