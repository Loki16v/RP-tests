using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ReportPortal.E2E.Core.Driver;

namespace ReportPortal.E2E.Core.Helpers
{
    public static class WaitFor
    {
        private static IWebDriver Driver => DriverFactory.GetDriver();
        private static WebDriverWait _wait;
        private static readonly TimeSpan ConditionTimeOutDefault = TimeSpan.FromSeconds(10);

        public static void Condition(Func<bool> condition, TimeSpan conditionTimeOut = default)
        {
            _wait = new WebDriverWait(Driver, conditionTimeOut == default(TimeSpan) ? ConditionTimeOutDefault : conditionTimeOut);
            _wait.Until(_ => condition.Invoke());
        }
    }
}
