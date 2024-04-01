using ReportPortal.E2E.UI.Core.Business.Helpers;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Core.Business.StepDefinitions.API
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
