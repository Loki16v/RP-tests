namespace ReportPortal.E2E.Core.Models
{
    public class JiraConfig
    {
        public string BaseUrl { get; set; }
        public string IssueEndpoint { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string IsAutomatedField { get; set; }
    }
}