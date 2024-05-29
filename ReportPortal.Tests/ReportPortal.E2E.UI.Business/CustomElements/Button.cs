using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.CustomElements
{
    public class Button : BaseElement
    {
        public Button(IWebElement element) : base(element) { }

        public void Click()
        {
            Element.Click();
        }

        public string GetButtonText()
        {
            var text = Element.Text;
            Log.LogInformation($"Got button text: '{text}'");
            return text;
        }
    }
}
