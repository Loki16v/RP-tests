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
            Log.LogInformation("Open dropdown menu and choose option '{Option}'.", option);
            Click();
            var optionElement = new DropDownOption(Element.FindElement(By.XPath(string.Format(OptionLocator, option))));
            Driver.WaitForCondition(() => optionElement.Enabled && optionElement.Displayed);
            optionElement.Click();
        }

    }
}
