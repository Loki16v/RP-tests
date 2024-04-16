using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.HttpMessageHandlers;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Models;
using ReportPortalApiSteps = ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers.ReportPortalApiSteps;

namespace ReportPortal.E2E.API.Business
{
    public static class Steps
    {
        private static ClientsHandler ClientsHandler =>
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<ClientsHandler>();

        private static UserCredentials AdminCredentials =>
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<UserCredentials>();

        private static readonly ILogger Log = TestsLogger.Create("LoginSteps");

        public static ReportPortalApiSteps AsAdminUser(bool refreshToken = false)
        {
            Log.LogInformation("Logging as SuperAdmin");
            var messageHandler = ClientsHandler
                .GetAuthHttpMessageHandler(AdminCredentials, refreshToken)
                .GetAwaiter()
                .GetResult();

            return new ReportPortalApiSteps(messageHandler);
        }

        public static ReportPortalApiSteps AsUser(UserCredentials user, bool refreshToken = false)
        {
            Log.LogInformation($"Logging as {user.UserName}");
            var messageHandler = ClientsHandler
                .GetAuthHttpMessageHandler(user, refreshToken)
                .GetAwaiter()
                .GetResult();

            return new ReportPortalApiSteps(messageHandler);
        }
    }
}
