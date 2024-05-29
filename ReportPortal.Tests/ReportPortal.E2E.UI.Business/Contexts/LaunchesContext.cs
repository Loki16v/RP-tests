using OpenQA.Selenium;
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

        public List<string> GetLaunchNames()
        {
            return GetLaunches().Select(item => item.GetName).ToList();
        }

        public LaunchesContext SelectLaunches(string option)
        {
            WaitForReady();
            LaunchesPage.AllLatestArrowButton.Click();
            LaunchesPage.LaunchesDropdownItem(option).WaitElementAndClick();
            return this;
        }

        public bool IsActionsEnabled()
        {
            return !LaunchesPage.ActionsButton.GetAttribute("class").Contains("disabled");
        }

        public LaunchesContext SortByName()
        {
            LaunchesPage.SortByNameButton.WaitElementAndClick();
            WaitForReady();
            return this;
        }

        public LaunchesContext SortByStartTime()
        {
            LaunchesPage.SortByStartTimeButton.WaitElementAndClick();
            WaitForReady();
            return this;
        }

        public LaunchesContext ScrollIntoLaunch(LaunchContainer launch)
        {
            launch.ScrollToElement();
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
            LaunchesPage.CompareButton.WaitElementAndClick();
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
            return LaunchesPage.CompareModal.Title.GetText();
        }

        public (int, int) GetCompareModalSize()
        {
            int.TryParse(LaunchesPage.CompareModal.Content.GetAttribute("height"), out int height);
            int.TryParse(LaunchesPage.CompareModal.Content.GetAttribute("width"), out int width);
            return (height, width);
        }

        public LaunchesContext CloseCompareModal()
        {
            LaunchesPage.CompareModal.CloseButton.JsClick();
            return this;
        }

        public bool IsModalDisappeared()
        {
            return LaunchesPage.WaitForCompareModalDisappear();
        }

        public LaunchesContext DeleteLaunchFromLaunchMenu(LaunchContainer launch)
        {
            launch.CommonMenuDropDown.OpenAndClickOption("Delete");
            return this;
        }

        public LaunchesContext ConfirmDeletion()
        {
            LaunchesPage.ConfirmationModal.DeleteButton.Click();
            LaunchesPage.WaitForReady();
            return this;
        }

        public LaunchDetailsContext NavigateToLaunchDetailsByNameLink(LaunchContainer launch)
        {
            launch.Name.Click();
            return new LaunchDetailsContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchAllTestsView(LaunchContainer launch)
        {
            launch.TotalCount.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchPassedTestsView(LaunchContainer launch)
        {
            launch.PassedCount.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchFailedTestsView(LaunchContainer launch)
        {
            launch.FailedCount.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchSkippedTestsView(LaunchContainer launch)
        {
            launch.SkippedCount.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchProductBugTestsView(LaunchContainer launch)
        {
            launch.ProductBug.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchAutomationBugTestsView(LaunchContainer launch)
        {
            launch.AutomationBug.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchSystemIssueTestsView(LaunchContainer launch)
        {
            launch.SystemIssue.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchTestsInfoContext NavigateToLaunchToInvestigateTestsView(LaunchContainer launch)
        {
            launch.ToInvestigate.JsClick();
            return new LaunchTestsInfoContext(Driver);
        }

        public LaunchesContext WaitForReady()
        {
            LaunchesPage.WaitForReady();
            return this;
        }
    }
}
