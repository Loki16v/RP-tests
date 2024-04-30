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
            if (Steps.AsAdminUser().SearchProjectUser<List<string>>(projectName, userName)
                .Contains(userName)) return new UserCredentials { UserName = userName, Password = DefaultPassword };

            var allUsers = Steps.AsAdminUser().SearchUsers<SearchUsersResponse>().UserList;
            if (allUsers.Any(x => x.UserId.Equals(userName)))
            {
                Steps.AsAdminUser().AddUserToProject<SuccessfulMessageResponse>(projectName, userName, userRole);
            }
            else
            {
                Steps.AsAdminUser().CreateUser(userName, DefaultPassword, projectName, email, fullName, userRole, accountRole);
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
            var projectsList = Steps.AsAdminUser().GetProjectsList<ProjectsListResponse>();
            if (projectsList.ProjectsList.Any(p => p.ProjectName.Equals(projectName))) return;
            var createProjectResponse = Steps.AsAdminUser().CreateProject<CreateProjectResponse>(projectName);
            CleanUpHelper.AddProjectId(createProjectResponse.ProjectId);
            Steps.AsAdminUser().CreateDemoData(projectName);
        }
    }
}