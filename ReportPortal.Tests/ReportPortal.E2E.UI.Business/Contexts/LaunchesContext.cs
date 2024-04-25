using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.Pages;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class LaunchesContext : BaseContext
    {
        public LaunchesContext(IWebDriver driver) : base(driver) { }

        private LaunchesPage LaunchesPage => new(Driver);

        public List<LaunchContainer> GetLaunches()
        {
            LaunchesPage.WaitForReady();
            return LaunchesPage.LaunchesList;
        }

        public LaunchesContext SelectLaunches(string option)
        {
            LaunchesPage.WaitForReady();
            LaunchesPage.AllLatestDropdownArrow.Click();
            Driver.WaitElementAndClick(LaunchesPage.LaunchesDropdownItem(option));
            return this;
        }

        public bool IsActionsEnabled()
        {
            return !LaunchesPage.ActionsButton.GetAttribute("class").Contains("disabled");
        }
    }
}
