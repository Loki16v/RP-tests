using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.CustomElements;

namespace ReportPortal.E2E.UI.Business.Pages.Modals
{
    internal class ConfirmationModal : BaseElement
    {
        public ConfirmationModal(IWebElement element) : base(element) { }

        private const string DeleteButtonLocator = ".//button[contains(text(),'Delete')]";

        internal Button DeleteButton => new(Element.FindElement(By.XPath(DeleteButtonLocator)));
    }
}
