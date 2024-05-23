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
            WaitForReady();
            return LaunchesPage.LaunchesList;
        }

        public List<string> GetLaunchesStartTime()
        {
            return GetLaunches().Select(item => item.StartTimeMarker.GetAttribute("textContent")).ToList();
        }

        public LaunchesContext SelectLaunches(string option)
        {
            WaitForReady();
            LaunchesPage.AllLatestDropdownArrow.Click();
            Driver.WaitElementAndClick(LaunchesPage.LaunchesDropdownItem(option));
            return this;
        }

        public bool IsActionsEnabled()
        {
            return !LaunchesPage.ActionsButton.GetAttribute("class").Contains("disabled");
        }

        public LaunchesContext SortByStartTime()
        {
            Driver.WaitElementAndClick(LaunchesPage.SortByStartTimeButton);
            WaitForReady();
            return this;
        }

        public LaunchesContext ScrollIntoLaunch(LaunchContainer launch)
        {
            Driver.ScrollToElement(launch.TotalCount);
            return this;
        }

        public LaunchesContext SelectLaunch(LaunchContainer launch)
        {
            launch.LaunchSelectCheckbox.Check();
            return this;
        }

        public LaunchesContext ClickCompareButton()
        {
            OpenActionsMenu();
            Driver.WaitElementAndClick(LaunchesPage.CompareButton);
            LaunchesPage.WaitForCompareModal();
            return this;
        }

        public LaunchesContext OpenActionsMenu()
        {
            LaunchesPage.ActionsButton.Click();
            return this;
        }

        public string GetCompareModalTitle()
        {
            return LaunchesPage.CompareModal.Title.Text;
        }

        public (int, int) GetCompareModalSize()
        {
            int.TryParse(LaunchesPage.CompareModal.Content.GetAttribute("height"), out int height);
            int.TryParse(LaunchesPage.CompareModal.Content.GetAttribute("width"), out int width);
            return (height, width);
        }

        public LaunchesContext CloseCompareModal()
        {
            Driver.JsClick(LaunchesPage.CompareModal.CloseButton);
            return this;
        }

        public bool IsModalDisappeared()
        {
            return LaunchesPage.WaitForCompareModalDisappear();
        }

        public LaunchesContext DeleteLaunchFromLaunchMenu(LaunchContainer launch)
        {
            launch.MenuButton.Click();
            Driver.WaitElementAndClick(launch.DeleteLaunchMenuButton);
            return this;
        }

        public LaunchesContext ConfirmDeletion()
        {
            LaunchesPage.ConfirmationModal.DeleteButton.Click();
            LaunchesPage.WaitForReady();
            return this;
        }

        public LaunchesContext WaitForReady()
        {
            LaunchesPage.WaitForReady();
            return this;
        }
    }
}
