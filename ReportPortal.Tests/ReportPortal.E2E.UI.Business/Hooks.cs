using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.API.Business.Helpers;
using ReportPortal.E2E.Core.Driver;
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
            PreconditionsHelper.CreateDefaultProjectAndUsers();
        }


        [AfterTestRun]
        public static void TearDown()
        {
            CleanUpHelper.CleanTestData();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            var browserType = TestContext.Parameters["Browser"];
            Log.LogInformation($"Start tests run with browser {browserType}");
            var driverFactory = new DriverFactory(browserType);
            featureContext.Add(ContextKeys.DriverFactory.ToString(), driverFactory);
            featureContext.Add(ContextKeys.LoginContext.ToString(), new LoginContext(driverFactory.GetDriver()));

            Log.LogInformation($"************************ Feature *{featureContext.FeatureInfo.Title}* starting ************************** \r\n");
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Log.LogInformation($"************************ Feature *{featureContext.FeatureInfo.Title}* ended *************************** \r\n\r\n");
            featureContext.Get<DriverFactory>(ContextKeys.DriverFactory.ToString()).CloseDriver();
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Log.LogInformation($"_______________________ Scenario *{scenarioContext.ScenarioInfo.Title}* starting ______________________ \r\n");
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            Log.LogInformation($"________________________ Scenario *{scenarioContext.ScenarioInfo.Title}* ended ________________________ \r\n");
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var stepType = $"{scenarioContext.StepContext.StepInfo.StepDefinitionType}";
            var state = scenarioContext.StepContext.TestError == null
                ? "| [SUCCESSFUL]"
                : $"| [FAILED : {scenarioContext.StepContext.TestError.Message}]";

            Log.LogInformation($"{stepType.ToUpper()} | {scenarioContext.StepContext.StepInfo.Text} {state}");
        }
    }
}
