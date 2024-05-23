using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Pages.Modals
{
    internal class CompareModal
    {
        private readonly IWebElement _element;

        public CompareModal(IWebElement element)
        {
            _element = element;
        }

        private const string TitleLocator = ".//*[contains(@class,'modalHeader__modal-title')]";
        private const string CloseButtonLocator = ".//*[contains(@class,'modalHeader__close-modal-icon')]";
        private const string ContentLocator = "//*[contains(@class,'launchesComparisonChart__launches-comparison-chart')]//*[name()='svg']";

        internal IWebElement Title => _element.FindElement(By.XPath(TitleLocator));
        internal IWebElement Content => _element.FindElement(By.XPath(ContentLocator));
        internal IWebElement CloseButton => _element.FindElement(By.XPath(CloseButtonLocator));
    }
}
