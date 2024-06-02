using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.Pages;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class LaunchDetailsContext : BaseContext
    {
        public LaunchDetailsContext(IWebDriver driver) : base(driver) { }

        private LaunchDetailsPage LaunchDetailsPage => new(Driver);

        public List<LaunchDetailContainer> GetLaunchRunDetails()
        {
            return LaunchDetailsPage.LaunchDetailsList;
        }
    }
}
