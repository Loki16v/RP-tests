using System.Collections.ObjectModel;
using OpenQA.Selenium;
using ReportPortal.E2E.UI.Core.Business.Helpers;

namespace ReportPortal.E2E.UI.Core.Business.Pages
{
    [PageUrl("/ui/#{projectName}/launches/all")]
    public class LaunchesPage : BasePage
    {
        internal ReadOnlyCollection<IWebElement> LaunchesList => Driver.FindElements(By.XPath("//div[contains(@class,'grid__grid')]/div[@data-id]"));
    }
}
