using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.Common.Logger;
using ReportPortal.E2E.UI.Core.Business.Driver;
using ReportPortal.E2E.UI.Core.Business.Helpers;
using TechTalk.SpecFlow;

namespace ReportPortal.E2E.UI.Core.Business
{
    [Binding]
    public class Hooks
    {
        private static readonly ILogger Log = TestsLogger.Create("ScenarioSteps");

        [BeforeTestRun]
        public static void SetUp()
        {
            var browserType = TestContext.Parameters["Browser"];
            Log.LogInformation($"Start tests run with browser {browserType}");
            DriverFactory.InitDriver(browserType);
        }


        [AfterTestRun]
        public static void TearDown()
        {
            DriverFactory.CloseDriver();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Log.LogInformation($"************************ Feature *{featureContext.FeatureInfo.Title}* starting ************************** \r\n");
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Log.LogInformation($"************************ Feature *{featureContext.FeatureInfo.Title}* ended *************************** \r\n\r\n");
            CleanUpHelper.CleanTestData();
        }

        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            Log.LogInformation($"_______________________ Scenario *{scenarioContext.ScenarioInfo.Title}* starting ______________________ \r\n");
        }

        [AfterScenario]
        public static void AfterScenario(ScenarioContext scenarioContext)
        {
            Log.LogInformation($"________________________ Scenario *{scenarioContext.ScenarioInfo.Title}* ended ________________________ \r\n");
        }

        [AfterStep]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            var stepType = $"{ScenarioStepContext.Current.StepInfo.StepDefinitionType}";
            var state = scenarioContext.TestError == null
                ? "| [SUCCESSFUL]"
                : $"| [FAILED : {scenarioContext.TestError.Message}]";

            Log.LogInformation($"{stepType.ToUpper()} | {ScenarioStepContext.Current.StepInfo.Text} {state}");
        }
    }
}
