using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.Core.Helpers
{
    public static class SlackHelper
    {
        private static readonly string SlackHooksBaseUrl = TestsBootstrap.Instance.Configuration.GetSection("SlackHooksBaseUrl").Value;
        private static readonly string SlackNotificationUrl = TestsBootstrap.Instance.Configuration.GetSection("SlackNotificationUrl").Value;


        public static void SendNotificationToSlack(string message)
        {
            if (SlackHooksBaseUrl is null)
            {

                return;
            }
            ApiClient.Get(SlackHooksBaseUrl).Post(SlackNotificationUrl, new
            {
                text = message
            });
        }
    }
}
