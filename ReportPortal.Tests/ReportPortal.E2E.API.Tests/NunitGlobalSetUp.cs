using NUnit.Framework;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.API.Tests
{
    [SetUpFixture]
    public class NunitGlobalSetUp
    {
        private const string NewUser = "new_user";
        protected const string ProjectName = "demo-project";
        internal static UserCredentials NewUserCredentials;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            PreconditionsHelper.CreateProjectWithDemoLaunches(ProjectName);
            NewUserCredentials = PreconditionsHelper.CreateNewUser(ProjectName, NewUser);
        }
    }
}
