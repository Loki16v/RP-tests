using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using ReportPortal.E2E.Core.Logger;

namespace ReportPortal.E2E.UI.Business.CustomElements
{
    public class Checkbox
    {
        private readonly IWebElement _element;
        private static readonly ILogger Log = TestsLogger.Create<Checkbox>();

        public Checkbox(IWebElement element)
        {
            _element = element;
        }

        public void Check()
        {
            if (!IsChecked())
            {
                Log.LogInformation("Checking checkbox.");
                _element.Click();
                return;
            }
            Log.LogInformation("Checkbox is already checked.");
        }

        public void Uncheck()
        {
            if (IsChecked())
            {
                Log.LogInformation("Uncheck checkbox.");
                _element.Click();
                return;
            }
            Log.LogInformation("Checkbox is already unchecked.");
        }

        public bool IsChecked()
        {
            var isChecked = _element.GetAttribute("class").Contains("checked");
            Log.LogInformation($"Checkbox is {(isChecked ? "checked." : "unchecked.")}");
        return isChecked;
        }
    }
}
