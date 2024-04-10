using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.API.Tests.Scenarios.NunitTest.BaseTest
{
    public abstract class BaseNunitTest
    {
        protected abstract void Preconditions();

        protected const string ProjectName = "demo-project";
        protected static UserCredentials NewUserCredentials = NunitGlobalSetUp.NewUserCredentials;

        protected ILogger Log { get; }

        private readonly SetUpHandler _setUpHandler = new();

        protected BaseNunitTest()
        {
            Log = TestsLogger.Create<BaseNunitTest>();
        }

        [OneTimeSetUp]
        public void Setup()
        {
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
