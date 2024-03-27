using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using ReportPortal.E2E.UI.Framework.Driver;

namespace ReportPortal.E2E.UI.Framework.Helpers
{
    public class JavaScriptLibrary
    {
        private static IWebDriver Driver => DriverFactory.GetDriver();

        public static void SetLocalStorageItem(string key, string valueInJsonFormat)
        {
            var func = $"localStorage.setItem('{key}', '{valueInJsonFormat}')";
            Driver.ExecuteJavaScript(func);
        }

        public static bool IsLocalStorageItemExist(string key)
        {
            var func = $"return localStorage.getItem('{key}')";
            return Driver.ExecuteJavaScript<string>(func) is not null;
        }

    }
}
