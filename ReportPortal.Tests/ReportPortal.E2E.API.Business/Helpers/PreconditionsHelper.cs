using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.API.Business.Helpers
{
    public static class PreconditionsHelper
    {
        private static string DefaultPassword =>
            TestsBootstrap.Instance.ServiceProvider.GetRequiredService<UserCredentials>().Password;

        public static UserCredentials CreateNewUser(string projectName, string userName, string userRole = "MEMBER",
            string email = null, string fullName = null, string accountRole = null)
        {
            if (Steps.AsAdminUser().SearchProjectUser(projectName, userName).GetAwaiter().GetResult()
                .GetResponse<List<string>>()
                .Contains(userName)) return new UserCredentials { UserName = userName, Password = DefaultPassword };

            var allUsers = Steps.AsAdminUser().SearchUsers().GetAwaiter().GetResult().GetResponse<SearchUsersResponse>().UserList;
            if (allUsers.Any(x => x.UserId.Equals(userName)))
            {
                var response = Steps.AsAdminUser().AddUserToProject(projectName, userName, userRole).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            else
            {
                var response = Steps.AsAdminUser().CreateUser(userName, DefaultPassword, projectName, email, fullName, userRole, accountRole).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            
            return new UserCredentials { UserName = userName, Password = DefaultPassword };
        }

        public static void CreateDefaultProjectAndUsers()
        {
            CreateProjectWithDemoLaunches(TestsBootstrap.Instance.Configuration.GetSection("DefaultProject").GetValueOrThrow());
            var users = TestsBootstrap.Instance.Configuration.GetSection("TestUsers").Get<UserModel[]>();

            foreach (var user in users)
            {
                CreateNewUser(user.DefaultProject, user.Login, user.ProjectRole, user.Email, user.FullName, user.AccountRole);
            }
        }

        public static void CreateProjectWithDemoLaunches(string projectName)
        {
            var projectsListResponse = Steps.AsAdminUser().GetProjectsList().GetAwaiter().GetResult();
            projectsListResponse.EnsureSuccessStatusCode();
            var projectsList = projectsListResponse.GetResponse<ProjectsListResponse>();
            if (projectsList.ProjectsList.Any(p => p.ProjectName.Equals(projectName))) return;
            Steps.AsAdminUser().CreateProject(projectName).GetAwaiter().GetResult();
            Steps.AsAdminUser().CreateDemoData(projectName).GetAwaiter().GetResult();
        }
    }
}