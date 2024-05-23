using OpenQA.Selenium;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Business.Pages;
using ReportPortal.E2E.UI.Business.Pages.Containers;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class DashboardChartsContext : BaseContext
    {
        public DashboardChartsContext(IWebDriver driver) : base(driver) { }

        private DashboardChartsPage DashboardChartsPage => new(Driver);

        public List<ChartContainer> GetCharts()
        {
            return DashboardChartsPage.Charts;
        }

        public ChartPosition GetPosition(ChartContainer chart)
        {
            var position = chart.GetPosition.Replace("matrix(", "").Replace(")", "").Split(", ");
            return new ChartPosition
            {
                Width = int.Parse(chart.GetWidthValue.Replace("px", "")),
                Height = int.Parse(chart.GetHeightValue.Replace("px", "")),
                X = (int)Math.Round(double.Parse(position[^2])),
                Y = (int)Math.Round(double.Parse(position[^1]))
            };
        }

        public DashboardChartsContext ResizeChart(ChartContainer chart, int x, int y)
        {
            Driver.DragAndDrop(chart.ResizeButton, x, y);
            return this;
        }

        public DashboardChartsContext MoveChart(ChartContainer chart, int x, int y)
        {
            Driver.DragAndDrop(chart.DraggableHeader, x, y);
            return this;
        }

        public DashboardChartsContext WaitForReady()
        {
            DashboardChartsPage.WaitForReady();
            return this;
        }
    }
}
