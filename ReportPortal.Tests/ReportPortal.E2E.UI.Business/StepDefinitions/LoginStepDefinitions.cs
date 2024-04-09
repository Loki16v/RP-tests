using ReportPortal.E2E.UI.Business.Helpers;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Business.StepDefinitions
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
