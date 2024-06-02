using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.CustomElements;

namespace ReportPortal.E2E.UI.Business.Pages.Modals
{
    internal class CompareModal : BaseElement
    {
        public CompareModal(IWebElement element) : base(element) { }

        private const string TitleLocator = ".//*[contains(@class,'modalHeader__modal-title')]";
        private const string CloseButtonLocator = ".//*[contains(@class,'modalHeader__close-modal-icon')]";
        private const string ContentLocator = "//*[contains(@class,'launchesComparisonChart__launches-comparison-chart')]//*[name()='svg']";

        internal Label Title => new(Element.FindElement(By.XPath(TitleLocator)));
        internal Label Content => new(Element.FindElement(By.XPath(ContentLocator)));
        internal Button CloseButton => new(Element.FindElement(By.XPath(CloseButtonLocator)));
    }
}
