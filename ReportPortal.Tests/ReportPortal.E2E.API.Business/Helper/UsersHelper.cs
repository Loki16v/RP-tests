using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.API.Business.Helper
{
    public static class UsersHelper
    {
        private static string DefaultPassword =>
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<UserCredentials>().Password;

        public static UserCredentials CreateNewUser(string projectName, string userName)
        {
            if (!Steps.AsAdminUser().SearchProjectUser(projectName, userName).GetAwaiter().GetResult()
                    .GetResponse<List<string>>().Contains(userName))
            {
                var response = Steps.AsAdminUser().CreateUser(userName, DefaultPassword, projectName).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            return new UserCredentials { UserName = userName, Password = DefaultPassword };
        }
    }
}