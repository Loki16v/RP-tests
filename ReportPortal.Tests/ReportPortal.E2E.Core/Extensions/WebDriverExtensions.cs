using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using ReportPortal.E2E.Core.Logger;

namespace ReportPortal.E2E.Core.Extensions
{
    public static class WebDriverExtensions
    {
        private static readonly int DefaultTimeout =
            TestsBootstrap.Instance.Configuration.GetSection("DefaultTimeout").Get<int>();
        private static readonly ILogger Log = TestsLogger.Create("WebDriverExtensions");

        public static void WaitForCondition(this IWebDriver driver, Func<bool> condition, TimeSpan conditionTimeOut = default)
        {
            var wait = new WebDriverWait(driver, conditionTimeOut == default
                ? TimeSpan.FromSeconds(DefaultTimeout)
                : conditionTimeOut);
            wait.Until(_ => condition.Invoke());
        }

        public static void SetLocalStorageItem(this IWebDriver driver, string key, string valueInJsonFormat)
        {
            var func = $"localStorage.setItem('{key}', '{valueInJsonFormat}')";
            driver.ExecuteJavaScript(func);
        }

        public static bool IsLocalStorageItemExist(this IWebDriver driver, string key)
        {
            var func = $"return localStorage.getItem('{key}')";
            return driver.ExecuteJavaScript<string>(func) is not null;
        }

        public static void ClearLocalStorage(this IWebDriver driver)
        {
            const string func = "localStorage.clear()";
            driver.ExecuteJavaScript(func);
        }

        public static bool ElementExistsByXPath(this IWebDriver driver, string xPath)
        {
            var func = $"return document.evaluate(\"{xPath}\", document, null, XPathResult.BOOLEAN_TYPE, null).booleanValue";
            return driver.ExecuteJavaScript<bool>(func);
        }

        public static void UpdateLambdaTestStatus(this IWebDriver driver, TestStatus status)
        {
            try
            {
                driver.ExecuteJavaScript("lambda-status=" + (status == TestStatus.Passed ? "passed" : "failed"));
            }
            catch (Exception ex)
            {
                Log.LogDebug($"Caught exception while executing script to update lambda test status: {ex.Message}");
            }
        }

        public static void TakeScreenShot(this IWebDriver driver)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var screenshotPath = Path.Combine(AppContext.BaseDirectory, "TestResults");
            if (!Directory.Exists(screenshotPath)) Directory.CreateDirectory(screenshotPath);
            screenshot.SaveAsFile(Path.Combine(screenshotPath,
                $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yy_MM_dd__HH_mm_ss}.png"));
        }
    }
}
