using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Helper;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.API.Tests.Scenarios.Nunit
{
    [SetUpFixture]
    public class GlobalSetupNunit
    {
        private const string NewUser = "new_user";
        protected const string ProjectName = "demo-project";
        public static UserCredentials NewUserCredentials;

        [OneTimeSetUp]
        public void Setup()
        {
            var companiesListResponse = Steps.AsAdminUser().GetProjectsList().GetAwaiter().GetResult();
            companiesListResponse.EnsureSuccessStatusCode();
            var projectsList = companiesListResponse.GetResponse<ProjectsListResponse>();
            if (!projectsList.ProjectsList.Any(p => p.ProjectName.Equals(ProjectName)))
            {
                Steps.AsAdminUser().CreateProject(ProjectName).GetAwaiter().GetResult();
                Steps.AsAdminUser().CreateDemoData(ProjectName).GetAwaiter().GetResult();
            }
            NewUserCredentials = UsersHelper.CreateNewUser(ProjectName, NewUser);
        }
    }
}
