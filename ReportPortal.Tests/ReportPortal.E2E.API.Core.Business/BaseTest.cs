﻿using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.API.Core.Business.Helpers;
using ReportPortal.E2E.API.Core.Business.Models.Responses;
using ReportPortal.E2E.Common.Extensions;
using ReportPortal.E2E.Common.Logger;
using ReportPortal.E2E.Common.Models;

namespace ReportPortal.E2E.API.Core.Business
{
    public abstract class BaseTest
    {
        private const string NewUser = "new_user";
        protected const string ProjectName = "demo-project";
        protected static UserCredentials NewUserCredentials;

        protected ILogger Log { get; }

        private readonly SetUpHandler _setUpHandler = new();

        protected BaseTest()
        {
            Log = TestsLogger.Create<BaseTest>();
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

        protected abstract void Preconditions();
    }
}
