using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.API.Tests.Scenarios.MsTest.BaseTest
{
    [TestClass]
    public abstract class MsTestBaseTest
    {
        private const string NewUser = "new_user";
        protected const string ProjectName = "demo-project";
        public static UserCredentials NewUserCredentials;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var projectsListResponse = Steps.AsAdminUser().GetProjectsList().GetAwaiter().GetResult();
            projectsListResponse.EnsureSuccessStatusCode();
            var projectsList = projectsListResponse.GetResponse<ProjectsListResponse>();
            if (!projectsList.ProjectsList.Any(p => p.ProjectName.Equals(ProjectName)))
            {
                Steps.AsAdminUser().CreateProject(ProjectName).GetAwaiter().GetResult();
                Steps.AsAdminUser().CreateDemoData(ProjectName).GetAwaiter().GetResult();
            }

            NewUserCredentials = UsersHelper.CreateNewUser(ProjectName, NewUser);
        }
    }
}