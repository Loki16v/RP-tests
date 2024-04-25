using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class LaunchesPage : BasePage
    {
        public const string Url = "/ui/#{projectName}/launches/all";
        private const string SpinnerLocator = "//*[contains(@class,'spinningPreloader')]";
        private const string LaunchContainerLocator = "//*[contains(@class,'grid__grid')]/*[@data-id]";
        private const string AllLatestDropdownArrowLocator = "//div[contains(@class,'allLatestDropdown__arrow')]";
        private const string ActionsButtonLocator = "//*[./span[contains(text(),'Actions')]]";
        private const string LaunchesDropdownItemLocator = "//div[contains(@class,'allLatestDropdown__option-list')]//div[contains(text(),'{0}')]";

        public LaunchesPage(IWebDriver driver) : base(driver) { }


        internal List<LaunchContainer> LaunchesList =>
            new(Driver.FindElements(By.XPath(LaunchContainerLocator))
                .Select(x=>new LaunchContainer(x)));

        internal IWebElement AllLatestDropdownArrow => Driver.FindElement(By.XPath(AllLatestDropdownArrowLocator));

        internal IWebElement LaunchesDropdownItem(string option) => Driver.FindElement(By.XPath(string.Format(LaunchesDropdownItemLocator, option)));

        internal IWebElement ActionsButton => Driver.FindElement(By.XPath(ActionsButtonLocator));
        
        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => AllLatestDropdownArrow.Displayed && !Driver.ElementExistsByXPath(SpinnerLocator));
        }
    }
}
