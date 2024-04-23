using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal abstract class BasePage
    {
        protected readonly IWebDriver Driver;

        internal abstract void WaitForReady();

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
