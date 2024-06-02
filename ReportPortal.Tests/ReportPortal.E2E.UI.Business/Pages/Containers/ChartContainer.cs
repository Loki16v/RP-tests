using OpenQA.Selenium;
using ReportPortal.E2E.UI.Business.CustomElements;

namespace ReportPortal.E2E.UI.Business.Pages.Containers
{
    public class ChartContainer : BaseElement
    {
        public ChartContainer(IWebElement element) : base(element) { }

        private const string ResizeButtonLocator = ".//*[contains(@class,'react-resizable-handle')]";
        private const string DraggableHeaderLocator = ".//*[contains(@class,'draggable-field')][contains(@class,'widget__modifiable')]";

        internal Button ResizeButton => new(Element.FindElement(By.XPath(ResizeButtonLocator)));

        internal Label DraggableHeader => new(Element.FindElement(By.XPath(DraggableHeaderLocator)));

        public string GetWidthValue => Element.GetCssValue("width");
        public string GetHeightValue => Element.GetCssValue("height");
        public string GetPosition => Element.GetCssValue("transform");
    }
}
