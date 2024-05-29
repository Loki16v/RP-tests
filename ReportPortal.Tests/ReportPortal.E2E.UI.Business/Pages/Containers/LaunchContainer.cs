using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.CustomElements;

namespace ReportPortal.E2E.UI.Business.Pages.Containers
{
    public class LaunchContainer : BaseElement
    {
        public LaunchContainer(IWebElement webElement) : base(webElement) { }

        private const string NameLocator = ".//*[contains(@class,'itemInfo__main-info')]";
        private const string TotalCountLocator = ".//*[contains(@class,'launchSuiteGrid__total-col')]//a";
        private const string PassedCountLocator = ".//*[contains(@class,'launchSuiteGrid__passed-col')]//a";
        private const string FailedCountLocator = ".//*[contains(@class,'launchSuiteGrid__failed-col')]//a";
        private const string SkippedCountLocator = ".//*[contains(@class,'launchSuiteGrid__skipped-col')]//a";
        private const string StartTimeMarkerLocator = ".//*[contains(@class,'absRelTime__absolute-time')]";
        private const string LaunchSelectCheckboxLocator = ".//*[contains(@class,'checkIcon__square')]";
        private const string MenuButtonLocator = ".//*[contains(@class,'hamburger__hamburger-icon')]";
        private const string ProductBugLocator = ".//*[contains(@class,'launchSuiteGrid__pb-col')]//a";
        private const string AutomationBugLocator = ".//*[contains(@class,'launchSuiteGrid__ab-col')]//a";
        private const string SystemIssueLocator = ".//*[contains(@class,'launchSuiteGrid__si-col')]//a";
        private const string ToInvestigateLocator = ".//*[contains(@class,'launchSuiteGrid__ti-col')]//a";

        internal Button Name => new(Element.FindElement(By.XPath(NameLocator)));
        internal Button TotalCount => new(Element.FindElement(By.XPath(TotalCountLocator)));
        internal Button PassedCount => new(Element.FindElement(By.XPath(PassedCountLocator)));
        internal Button FailedCount => new(Element.FindElement(By.XPath(FailedCountLocator)));
        internal Button SkippedCount => new(Element.FindElement(By.XPath(SkippedCountLocator)));
        internal Button ProductBug => new(Element.FindElement(By.XPath(ProductBugLocator)));
        internal Button AutomationBug => new(Element.FindElement(By.XPath(AutomationBugLocator)));
        internal Button SystemIssue => new(Element.FindElement(By.XPath(SystemIssueLocator)));
        internal Button ToInvestigate => new(Element.FindElement(By.XPath(ToInvestigateLocator)));
        internal Label StartTimeMarker => new(Element.FindElement(By.XPath(StartTimeMarkerLocator)));
        internal Checkbox LaunchSelectCheckbox => new(Element.FindElement(By.XPath(LaunchSelectCheckboxLocator)));
        internal DropDown CommonMenuDropDown => new(Element.FindElement(By.XPath(MenuButtonLocator)));

        public string GetName => Name.GetButtonText();
        
    }
}
