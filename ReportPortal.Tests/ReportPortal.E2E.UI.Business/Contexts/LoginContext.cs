using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OpenQA.Selenium;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.HttpMessageHandlers;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.UI.Business.Contexts
{
    public class LoginContext : BaseContext
    {
        public LoginContext(IWebDriver driver) : base(driver) { }

        private static readonly UserCredentials AdminUser =
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<UserCredentials>();

        private string _currentUser;

        private const string TokenKey = "token";
        private const string ApplicationSettingsKey = "applicationSettings";
        private NavigationContext NavigationContext => new(Driver);

        public void LoginAsAdmin()
        {
            LoginAs(AdminUser.UserName);
        }

        public void LoginAs(string user)
        {
            if (user.Equals(_currentUser)) return;
            if (_currentUser != null) Logout();
            PerformLogin(new UserCredentials { UserName = user, Password = AdminUser.Password });
            _currentUser = user;
        }


        #region Private Methods

        private void PerformLogin(UserCredentials user)
        {
            var tokenInfo = new ClientsHandler()
                .CreateAuthToken(user).GetAwaiter().GetResult();
            var token = JsonConvert.SerializeObject(new
            {
                type = tokenInfo.TokenType,
                value = tokenInfo.AccessToken
            });
            NavigationContext.GoToReportPortalBaseUrl();
            Driver.SetLocalStorageItem(TokenKey, token);
            NavigationContext.GoToReportPortalBaseUrl();

            Driver.WaitForCondition(() => Driver.IsLocalStorageItemExist(ApplicationSettingsKey));
        }

        private void Logout()
        {
            Driver.ClearLocalStorage();
        }

        #endregion

    }
}
