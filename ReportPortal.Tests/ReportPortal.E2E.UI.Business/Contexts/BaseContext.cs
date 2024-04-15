using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public abstract class BaseContext
    {
        protected readonly IWebDriver Driver;

        protected BaseContext(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
