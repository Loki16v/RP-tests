using NUnit.Framework;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.API.Tests
{
    [SetUpFixture]
    public static class NunitGlobalSetUp
    {
        private const string NewUser = "new_user";
        public const string ProjectName = "demo-project";
        public static UserCredentials NewUserCredentials { get; set; }

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            PreconditionsHelper.CreateProjectWithDemoLaunches(ProjectName);
            NewUserCredentials = PreconditionsHelper.CreateNewUser(ProjectName, NewUser);
        }
    }
}
