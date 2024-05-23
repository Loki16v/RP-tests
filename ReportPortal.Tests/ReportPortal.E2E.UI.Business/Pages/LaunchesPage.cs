using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.Pages.Containers;
using ReportPortal.E2E.UI.Business.Pages.Modals;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class LaunchesPage : BasePage
    {
        public const string Url = "/ui/#{projectName}/launches/all";

        private const string LaunchContainerLocator = "//*[contains(@class,'grid__grid')]/*[@data-id]";
        private const string AllLatestDropdownArrowLocator = "//div[contains(@class,'allLatestDropdown__arrow')]";
        private const string ActionsButtonLocator = "//*[./span[contains(text(),'Actions')]]";
        private const string LaunchesDropdownItemLocator = "//div[contains(@class,'allLatestDropdown__option-list')]//div[contains(text(),'{0}')]";
        private const string SortByStartTimeButtonLocator = "//*[contains(@class,'headerCell__title-container')][.//*[contains(text(),'start')]]";
        private const string CompareModalLocator = "//*[contains(@class,'launchCompareModal__launch-compare-modal')]";
        private const string CompareButtonLocator = "//*[text()='Compare']";
        private const string ConfirmationModalLocator = "//*[contains(@class,'modalLayout__modal-window')]";

        public LaunchesPage(IWebDriver driver) : base(driver) { }


        internal List<LaunchContainer> LaunchesList =>
            new(Driver.FindElements(By.XPath(LaunchContainerLocator))
                .Select(x=>new LaunchContainer(x)));

        internal IWebElement AllLatestDropdownArrow => Driver.FindElement(By.XPath(AllLatestDropdownArrowLocator));

        internal IWebElement LaunchesDropdownItem(string option) => Driver.FindElement(By.XPath(string.Format(LaunchesDropdownItemLocator, option)));

        internal IWebElement SortByStartTimeButton => Driver.FindElement(By.XPath(SortByStartTimeButtonLocator));

        internal IWebElement ActionsButton => Driver.FindElement(By.XPath(ActionsButtonLocator));
        internal IWebElement CompareButton => Driver.FindElement(By.XPath(CompareButtonLocator));

        internal CompareModal CompareModal => new(Driver.FindElement(By.XPath(CompareModalLocator)));
        internal ConfirmationModal ConfirmationModal => new(Driver.FindElement(By.XPath(ConfirmationModalLocator)));


        internal void WaitForCompareModal()
        {
            Driver.WaitForElementToAppear(By.XPath(CompareModalLocator));
        }

        public bool WaitForCompareModalDisappear()
        {
            return Driver.WaitForElementToDisappear(CompareModal.Content);
        }

        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => LaunchesList.Count !=0 && !Driver.ElementExistsByXPath(SpinnerLocator));
        }
    }
}
