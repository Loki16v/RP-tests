using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Logger;

namespace ReportPortal.E2E.UI.Business.CustomElements
{
    public abstract class BaseElement
    {
        private static readonly int DefaultTimeout =
            TestsBootstrap.Instance.Configuration.GetSection("DefaultTimeout").Get<int>();

        protected readonly ILogger Log;
        protected IWebElement Element;
        protected IWebDriver Driver;

        protected BaseElement(IWebElement element)
        {
            Element = element;
            Driver = ((IWrapsDriver)Element).WrappedDriver;
            Log = TestsLogger.Create<BaseElement>();
        }

        public bool IsDisplayed()
        {
            return Element.Displayed;
        }

        public void WaitElementAndClick()
        {
            Driver.WaitForCondition(() => Element.Displayed);
            Element.Click();
        }

        public void JsClick()
        {
            Driver.ExecuteJavaScript("arguments[0].click();", Element);
        }

        public string GetAttribute(string attributeName)
        {
            return Element.GetAttribute(attributeName);
        }

        public void DragAndDrop(int x, int y)
        {
            new Actions(Driver).DragAndDropToOffset(Element, x, y).Perform();
        }


        public void ScrollToElement()
        {
            if (!IsElementIntoView())
               Driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", Element);
        }

        public bool IsElementIntoView()
        {
            return Driver.ExecuteJavaScript<bool>(
                "var rect = arguments[0].getBoundingClientRect();" +
                "return (" +
                "rect.top >= 0 &&" +
                "rect.left >= 0 &&" +
                "rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&" +
                "rect.right <= (window.innerWidth || document.documentElement.clientWidth)" +
                ");",
                Element);
        }

        public BaseElement WaitUntilAppear()
        {
            var fluentWait = new DefaultWait<IWebDriver>(Driver)
            {
                Timeout = TimeSpan.FromSeconds(DefaultTimeout),
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            fluentWait.Until(_ => Element.Displayed);
            return this;
        }

        public bool WaitUntilDisappear()
        {
            var wait = new DefaultWait<IWebDriver>(Driver)
            {
                Timeout = TimeSpan.FromSeconds(DefaultTimeout),
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };

            return wait.Until(_ =>
            {
                try
                {
                    return !Element.Displayed;
                }
                catch (Exception ex) when (ex is NoSuchElementException or StaleElementReferenceException)
                {
                    return true;
                }
            });
        }
    }
}
