using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.CustomElements;
using ReportPortal.E2E.UI.Business.Pages.Containers;
using ReportPortal.E2E.UI.Business.Pages.Modals;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal class LaunchesPage : BasePage
    {
        public const string Url = "/ui/#{projectName}/launches/all";

        private const string LaunchContainerLocator = "//*[contains(@class,'grid__grid')]/*[@data-id]";
        private const string AllLatestDropdownLocator = "//*[contains(@class,'allLatestDropdown__arrow')]";
        private const string LaunchesDropdownItemLocator = "//div[contains(@class,'allLatestDropdown__option-list')]//div[contains(text(),'{0}')]";
        private const string ActionsButtonLocator = "//*[./span[contains(text(),'Actions')]]";
        private const string SortByNameButtonLocator = "//*[contains(@class,'headerCell__title-container')][.//*[contains(text(),'name')]]";
        private const string SortByStartTimeButtonLocator = "//*[contains(@class,'headerCell__title-container')][.//*[contains(text(),'start')]]";
        private const string CompareModalLocator = "//*[contains(@class,'launchCompareModal__launch-compare-modal')]";
        private const string CompareButtonLocator = "//*[text()='Compare']";
        private const string ConfirmationModalLocator = "//*[contains(@class,'modalLayout__modal-window')]";

        public LaunchesPage(IWebDriver driver) : base(driver) { }


        internal List<LaunchContainer> LaunchesList =>
            new(Driver.FindElements(By.XPath(LaunchContainerLocator))
                .Select(x=>new LaunchContainer(x)));

        internal Button AllLatestArrowButton => new(Driver.FindElement(By.XPath(AllLatestDropdownLocator)));

        internal Button LaunchesDropdownItem(string option) => new(Driver.FindElement(By.XPath(string.Format(LaunchesDropdownItemLocator, option))));

        internal Button SortByNameButton => new(Driver.FindElement(By.XPath(SortByNameButtonLocator)));

        internal Button SortByStartTimeButton => new(Driver.FindElement(By.XPath(SortByStartTimeButtonLocator)));

        internal Button ActionsButton => new(Driver.FindElement(By.XPath(ActionsButtonLocator)));
        internal Button CompareButton => new(Driver.FindElement(By.XPath(CompareButtonLocator)));

        internal CompareModal CompareModal => new(Driver.FindElement(By.XPath(CompareModalLocator)));
        internal ConfirmationModal ConfirmationModal => new(Driver.FindElement(By.XPath(ConfirmationModalLocator)));


        internal void WaitForCompareModal()
        {
            CompareModal.WaitUntilAppear();
        }

        public bool WaitForCompareModalDisappear()
        {
            return CompareModal.WaitUntilDisappear();
        }

        internal override void WaitForReady()
        {
            Driver.WaitForCondition(() => LaunchesList.Count !=0 && !Driver.ElementExistsByXPath(SpinnerLocator));
        }
    }
}
