using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.CustomElements
{
    public class Checkbox : BaseElement
    {
        public Checkbox(IWebElement element) : base(element) { }

        public void Check()
        {
            if (!IsChecked())
            {
                Log.LogInformation("Checking checkbox.");
                Click();
                return;
            }
            Log.LogDebug("Checkbox is already checked.");
        }

        public void Uncheck()
        {
            if (IsChecked())
            {
                Log.LogInformation("Uncheck checkbox.");
                Click();
                return;
            }
            Log.LogDebug("Checkbox is already unchecked.");
        }

        public bool IsChecked()
        {
            var isChecked = Element.GetAttribute("class").Contains("checked");
            Log.LogInformation("Checkbox is {IsChecked}", (isChecked ? "checked." : "unchecked."));
            return isChecked;
        }
    }
}
