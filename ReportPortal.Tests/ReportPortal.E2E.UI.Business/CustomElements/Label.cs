using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.CustomElements
{
    public class Label : BaseElement
    {
        public Label(IWebElement element) : base(element) { }

        public string GetText()
        {
            var text = Element.Text;
            Log.LogInformation($"Got element text: '{text}'");
            return text;
        }
    }
}
