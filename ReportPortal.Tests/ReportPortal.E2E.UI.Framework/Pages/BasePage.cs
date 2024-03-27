using OpenQA.Selenium;
using ReportPortal.E2E.UI.Framework.Driver;

namespace ReportPortal.E2E.UI.Framework.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver Driver = DriverFactory.GetDriver();
    }
}
