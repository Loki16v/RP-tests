using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.HttpMessageHandlers;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Framework.Contexts;
using ReportPortal.E2E.UI.Framework.Helpers;

namespace ReportPortal.E2E.UI.Tests.Helpers
{
    public static class LoginHelper
    {
        private static readonly UserCredentials AdminUser =
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<UserCredentials>();

        private const string TokenKey = "token";
        private const string ApplicationSettingsKey = "applicationSettings";
        private static NavigationContext NavigationContext => new();

        public static void LoginAsAdmin()
        {
            PerformLogin(AdminUser);
        }

        public static void LoginAs(UserCredentials user)
        {
            PerformLogin(user);
        }

        private static void PerformLogin(UserCredentials user)
        {
            var tokenInfo = new ClientsHandler()
                .CreateAuthToken(user).GetAwaiter().GetResult();
            var token = JsonConvert.SerializeObject(new
            {
                type = tokenInfo.TokenType,
                value = tokenInfo.AccessToken
            });
            NavigationContext.GoToReportPortalBaseUrl();
            JavaScriptLibrary.SetLocalStorageItem(TokenKey, token);
            NavigationContext.GoToReportPortalBaseUrl();

            WaitFor.Condition(() => JavaScriptLibrary.IsLocalStorageItemExist(ApplicationSettingsKey));
        }
    }
}
