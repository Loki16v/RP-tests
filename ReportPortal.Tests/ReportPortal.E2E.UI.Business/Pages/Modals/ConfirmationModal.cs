using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Pages.Modals
{
    internal class ConfirmationModal
    {
        private readonly IWebElement _element;

        public ConfirmationModal(IWebElement element)
        {
            _element = element;
        }

        private const string DeleteButtonLocator = ".//button[contains(text(),'Delete')]";

        internal IWebElement DeleteButton => _element.FindElement(By.XPath(DeleteButtonLocator));
    }
}
