using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportPortal.E2E.API.Core.Business.Helpers;
using ReportPortal.E2E.API.Core.Business.Models.Responses;
using ReportPortal.E2E.Common.Extensions;
using ReportPortal.E2E.Common.Models;

namespace ReportPortal.E2E.API.Core.Business
{
    public abstract class MsTestBaseTest
    {
        private const string NewUser = "new_user";
        protected const string ProjectName = "demo-project";
        protected static UserCredentials NewUserCredentials;

        [TestInitialize]
        public void TestInitialize()
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
