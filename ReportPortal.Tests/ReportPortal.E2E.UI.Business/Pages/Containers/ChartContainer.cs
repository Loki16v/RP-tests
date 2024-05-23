using OpenQA.Selenium;

namespace ReportPortal.E2E.UI.Business.Pages.Containers
{
    public class ChartContainer
    {
        private readonly IWebElement _element;

        public ChartContainer(IWebElement element)
        {
            _element = element;
        }

        private const string ResizeButtonLocator = ".//*[contains(@class,'react-resizable-handle')]";
        private const string DraggableHeaderLocator = ".//*[contains(@class,'draggable-field')][contains(@class,'widget__modifiable')]";

        internal IWebElement ResizeButton =>
            _element.FindElement(By.XPath(ResizeButtonLocator));

        internal IWebElement DraggableHeader =>
            _element.FindElement(By.XPath(DraggableHeaderLocator));

        public string GetWidthValue => _element.GetCssValue("width");
        public string GetHeightValue => _element.GetCssValue("height");
        public string GetPosition => _element.GetCssValue("transform");
    }
}
