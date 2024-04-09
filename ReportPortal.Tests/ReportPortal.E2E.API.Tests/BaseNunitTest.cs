using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.API.Tests
{
    public abstract class BaseNunitTest
    {
        protected abstract void Preconditions();

        private const string NewUser = "new_user";
        protected const string ProjectName = "demo-project";
        protected static UserCredentials NewUserCredentials;

        protected ILogger Log { get; }

        private readonly SetUpHandler _setUpHandler = new();

        protected BaseNunitTest()
        {
            Log = TestsLogger.Create<BaseNunitTest>();
        }

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

            _setUpHandler.Do(new[] { Preconditions });
            Log.LogInformation($"{TestContext.CurrentContext.Test.FullName} started");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            CleanUpHelper.CleanTestData();
        }
    }
}
