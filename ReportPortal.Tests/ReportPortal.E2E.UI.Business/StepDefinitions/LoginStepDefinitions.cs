using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Business.Contexts;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Business.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        public LoginStepDefinitions(FeatureContext featureContext)
        {
            _loginContext = featureContext.Get<LoginContext>(ContextKeys.LoginContext.ToString());
        }

        private readonly LoginContext _loginContext;

        [Given(@"I am logged in as SuperAdmin")]
        public void GivenIAmLoggedInAsSuperAdmin()
        {
            _loginContext.LoginAsAdmin();
        }

        [Given(@"I am logged in as '([^']*)'")]
        public void GivenIAmLoggedInAs(string userName)
        {
            _loginContext.LoginAs(userName);
        }
    }
}
