using System.Collections.ObjectModel;
using OpenQA.Selenium;
using ReportPortal.E2E.UI.Core.Business.Pages;

namespace ReportPortal.E2E.UI.Core.Business.Contexts
{
    public class LaunchesContext
    {
        private LaunchesPage LaunchesPage => new();

        public ReadOnlyCollection<IWebElement> GetLaunches()
        {
            return LaunchesPage.LaunchesList;
        }
    }
}
