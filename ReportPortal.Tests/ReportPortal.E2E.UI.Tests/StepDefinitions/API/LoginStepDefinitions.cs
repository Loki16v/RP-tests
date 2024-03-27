using ReportPortal.E2E.UI.Tests.Helpers;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Tests.StepDefinitions.API
{
    [Binding]
    public class LoginStepDefinitions
    {
        [Given(@"I am logged in as SuperAdmin")]
        public void GivenIAmLoggedInAsSuperAdmin()
        {
            LoginHelper.LoginAsAdmin();
        }
    }
}
