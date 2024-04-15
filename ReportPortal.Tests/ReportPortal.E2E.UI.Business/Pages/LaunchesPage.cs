﻿using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Pages
{
    public class LaunchesPage : BasePage
    {
        public const string Url = "/ui/#{projectName}/launches/all";

        public LaunchesPage(IWebDriver driver) : base(driver) { }

        internal List<LaunchContainer> LaunchesList =>
            new(Driver.FindElements(By.XPath("//*[contains(@class,'grid__grid')]/*[@data-id]"))
                .Select(x=>new LaunchContainer(x)));

        internal IWebElement AllLatestDropdownArrow =>
            Driver.FindElement(By.XPath("//div[contains(@class,'allLatestDropdown__arrow')]"));

        internal IWebElement LaunchesDropdownItem(string option) => Driver.FindElement(By.XPath($"//div[contains(@class,'allLatestDropdown__option-list')]//div[contains(text(),'{option}')]"));
        
        
        public override void WaitForReady()
        {
            Driver.WaitForCondition(() => AllLatestDropdownArrow.Displayed && !Driver.ElementExistsByXPath("//*[contains(@class,'spinningPreloader')]"));
        }
    }
}
