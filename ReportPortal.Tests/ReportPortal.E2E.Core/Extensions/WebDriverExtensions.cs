using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace ReportPortal.E2E.Core.Extensions
{
    public static class WebDriverExtensions
    {
        private static readonly int DefaultTimeout =
            TestsBootstrap.Instance.Configuration.GetSection("DefaultTimeout").Get<int>();
        
        public static void WaitForCondition(this IWebDriver driver, Func<bool> condition, TimeSpan conditionTimeOut = default)
        {
            var wait = new WebDriverWait(driver, conditionTimeOut == default
                ? TimeSpan.FromSeconds(DefaultTimeout)
                : conditionTimeOut);
            wait.Until(_ => condition.Invoke());
        }

        public static void WaitElementAndClick(this IWebDriver driver, IWebElement element)
        {
            driver.WaitForCondition(() => element.Displayed);
            element.Click();
        }

        public static IWebElement WaitForElementToAppear(this IWebDriver driver, By locator)
        {
            var fluentWait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(DefaultTimeout),
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return fluentWait.Until(x => x.FindElement(locator));
        }

        public static void JsClick(this IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        public static void DragAndDrop(this IWebDriver driver, IWebElement element, int x, int y)
        {
            new Actions(driver).DragAndDropToOffset(element,x,y).Perform();
        }

        public static bool WaitForElementToDisappear(this IWebDriver driver, IWebElement element)
        {
            var wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(DefaultTimeout),
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };

            return wait.Until(_ =>
            {
                try
                {
                    return !element.Displayed;
                }
                catch (Exception ex) when (ex is NoSuchElementException or StaleElementReferenceException)
                {
                    return true;
                }
            });
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            if(!driver.IsElementIntoView(element))
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static bool IsElementIntoView(this IWebDriver driver, IWebElement element)
        {
            return (bool)((IJavaScriptExecutor)driver).ExecuteScript(
                "var rect = arguments[0].getBoundingClientRect();" +
                "return (" +
                "rect.top >= 0 &&" +
                "rect.left >= 0 &&" +
                "rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&" +
                "rect.right <= (window.innerWidth || document.documentElement.clientWidth)" +
                ");",
                element);
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
    }
}
