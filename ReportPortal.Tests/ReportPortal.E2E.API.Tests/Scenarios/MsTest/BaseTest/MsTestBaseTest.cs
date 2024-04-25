using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportPortal.E2E.API.Business.Helpers;
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
            PreconditionsHelper.CreateProjectWithDemoLaunches(ProjectName);
            NewUserCredentials = PreconditionsHelper.CreateNewUser(ProjectName, NewUser);
        }
    }
}