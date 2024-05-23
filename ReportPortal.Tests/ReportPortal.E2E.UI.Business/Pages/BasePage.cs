using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Pages
{
    internal abstract class BasePage
    {
        protected readonly IWebDriver Driver;

        protected const string SpinnerLocator = "//*[contains(@class,'spinningPreloader')]";

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal abstract void WaitForReady();
    }
}
