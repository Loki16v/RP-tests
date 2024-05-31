using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;

namespace ReportPortal.E2E.UI.Business.CustomElements
{
    public class DropDown : BaseElement
    {
        public DropDown(IWebElement element) : base(element) { }

        private const string OptionLocator = "..//*[text()='{0}']";

        public void OpenAndClickOption(string option)
        {
            Log.LogInformation($"Open dropdown menu and choose option '{option}'.");
            var locator = By.XPath(string.Format(OptionLocator, option));
            Click();
            Driver.WaitForCondition(() => Element.FindElement(locator).Displayed);
            Element.FindElement(locator).Click();
        }

    }
}
