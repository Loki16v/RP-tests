using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;

        public abstract void WaitForReady();

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
