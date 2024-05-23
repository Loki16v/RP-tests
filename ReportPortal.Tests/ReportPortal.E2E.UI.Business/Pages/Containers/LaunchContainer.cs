using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.CustomElements;

namespace ReportPortal.E2E.UI.Business.Pages.Containers
{
    public class LaunchContainer
    {
        private readonly IWebElement _webElement;

        public LaunchContainer(IWebElement webElement)
        {
            _webElement = webElement;
        }

        private const string NameLocator = ".//*[contains(@class,'itemInfo__main-info')]";
        private const string TotalCountLocator = ".//*[contains(@class,'launchSuiteGrid__total-col')]//a";
        private const string PassedCountLocator = ".//*[contains(@class,'launchSuiteGrid__passed-col')]//a";
        private const string FailedCountLocator = ".//*[contains(@class,'launchSuiteGrid__failed-col')]//a";
        private const string SkippedCountLocator = ".//*[contains(@class,'launchSuiteGrid__skipped-col')]//a";
        private const string StartTimeMarkerLocator = ".//*[contains(@class,'absRelTime__absolute-time')]";
        private const string LaunchSelectCheckboxLocator = ".//*[contains(@class,'checkIcon__square')]";
        private const string MenuButtonLocator = ".//*[contains(@class,'hamburger__hamburger-icon')]";
        private const string DeleteLaunchMenuButtonLocator = ".//*[contains(text(),'Delete')]";

        internal IWebElement Name => _webElement.FindElement(By.XPath(NameLocator));
        internal IWebElement TotalCount =>
            _webElement.FindElement(By.XPath(TotalCountLocator));
        internal IWebElement PassedCount =>
            _webElement.FindElement(By.XPath(PassedCountLocator));
        internal IWebElement FailedCount =>
            _webElement.FindElement(By.XPath(FailedCountLocator));
        internal IWebElement SkippedCount =>
            _webElement.FindElement(By.XPath(SkippedCountLocator));

        internal IWebElement StartTimeMarker =>
            _webElement.FindElement(By.XPath(StartTimeMarkerLocator));

        internal Checkbox LaunchSelectCheckbox =>
            new(_webElement.FindElement(By.XPath(LaunchSelectCheckboxLocator)));

        internal IWebElement MenuButton => _webElement.FindElement(By.XPath(MenuButtonLocator));
        
        internal IWebElement DeleteLaunchMenuButton => _webElement.FindElement(By.XPath(DeleteLaunchMenuButtonLocator));

        public string GetName => Name.Text;
        
    }
}
