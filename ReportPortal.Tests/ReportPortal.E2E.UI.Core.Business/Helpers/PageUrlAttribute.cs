namespace ReportPortal.E2E.UI.Core.Business.Helpers
{
    public class PageUrlAttribute : Attribute
    {
        public PageUrlAttribute(string pageUrl) => this.Url = (pageUrl);

        public string Url { get; private set; }
    }
}
