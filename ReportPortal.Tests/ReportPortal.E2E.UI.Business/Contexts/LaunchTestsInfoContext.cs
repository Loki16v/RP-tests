using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.Pages;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class LaunchTestsInfoContext : BaseContext
    {
        public LaunchTestsInfoContext(IWebDriver driver) : base(driver) { }
        private LaunchTestsInfoPage LaunchTestsInfoPage => new(Driver);

        public List<TestInfoContainer> GetTestsInfo()
        {
            return LaunchTestsInfoPage.TestsInfoList;
        }
    }
}
