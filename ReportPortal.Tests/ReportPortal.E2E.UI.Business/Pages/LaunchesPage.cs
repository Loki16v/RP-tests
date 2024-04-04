using System.Collections.ObjectModel;
using OpenQA.Selenium;
using ReportPortal.E2E.Core.Helpers;

namespace ReportPortal.E2E.UI.Business.Pages
{
    [PageUrl("/ui/#{projectName}/launches/all")]
    public class LaunchesPage : BasePage
    {
        internal ReadOnlyCollection<IWebElement> LaunchesList => Driver.FindElements(By.XPath("//*[contains(@class,'grid__grid')]/*[@data-id]"));
    }
}
