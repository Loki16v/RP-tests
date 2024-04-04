using OpenQA.Selenium;
using ReportPortal.E2E.Core.Driver;

namespace ReportPortal.E2E.UI.Business.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver = DriverFactory.GetDriver();
    }
}
