using OpenQA.Selenium;
using ReportPortal.E2E.UI.Core.Business.Driver;

namespace ReportPortal.E2E.UI.Core.Business.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver = DriverFactory.GetDriver();
    }
}
