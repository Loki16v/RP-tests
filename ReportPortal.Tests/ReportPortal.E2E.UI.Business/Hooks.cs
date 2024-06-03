using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.Core.Driver;
using ReportPortal.E2E.Core.Helpers;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.UI.Business.Contexts;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Business
{
    [Binding]
    public class Hooks
    {
        private static readonly ILogger Log = TestsLogger.Create("ScenarioSteps");

        [BeforeTestRun]
        public static void SetUp()
        {
            SlackHelper.SendNotificationToSlack("=== BDD TEST RUN STARTED ===");
            PreconditionsHelper.CreateDefaultProjectAndUsers();
        }


        [AfterTestRun]
        public static void TearDown()
        {
            CleanUpHelper.CleanTestData();
            SlackHelper.SendNotificationToSlack("=== BDD TEST RUN FINISHED ===");
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            var browserType = TestContext.Parameters["Browser"];
            Log.LogInformation("Start tests run with browser {BrowserType}", browserType);
            var driverFactory = new DriverFactory(browserType);
            featureContext.Add(ContextKeys.DriverFactory.ToString(), driverFactory);
            featureContext.Add(ContextKeys.LoginContext.ToString(), new LoginContext(driverFactory.GetDriver()));

            Log.LogInformation("************************ Feature *{Title}* starting ************************** \r\n",
                featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Log.LogInformation("************************ Feature *{Title}* ended *************************** \r\n\r\n",
                featureContext.FeatureInfo.Title);
            featureContext.Get<DriverFactory>(ContextKeys.DriverFactory.ToString()).CloseDriver();
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Log.LogInformation("_______________________ Scenario *{Title}* starting ______________________ \r\n",
                scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            Log.LogInformation("________________________ Scenario *{Title}* ended ________________________ \r\n",
                scenarioContext.ScenarioInfo.Title);
            SlackHelper.SendNotificationToSlack($"{scenarioContext.ScenarioInfo.Title} : {(scenarioContext.TestError == null ? "PASSED" : "FAILED")}");
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var stepType = $"{scenarioContext.StepContext.StepInfo.StepDefinitionType}";
            var state = scenarioContext.StepContext.TestError == null
                ? "| [SUCCESSFUL]"
                : $"| [FAILED : {scenarioContext.StepContext.TestError.Message}]";

            Log.LogInformation("{StepType} | {StepText} {State}",
                stepType.ToUpper(), scenarioContext.StepContext.StepInfo.Text, state);
        }
    }
}
