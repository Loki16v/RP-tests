using System.Collections.ObjectModel;
using OpenQA.Selenium;
using ReportPortal.E2E.UI.Framework.Pages;

namespace ReportPortal.E2E.UI.Framework.Contexts
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
