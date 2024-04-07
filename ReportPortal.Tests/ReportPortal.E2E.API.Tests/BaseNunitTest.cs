using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.Core.Logger;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.API.Tests
{
    public abstract class BaseNunitTest
    {
        protected ILogger Log { get; }

        private readonly SetUpHandler _setUpHandler = new();

        protected BaseNunitTest()
        {
            Log = TestsLogger.Create<BaseNunitTest>();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _setUpHandler.Do(new[] { Preconditions });
            Log.LogInformation($"{TestContext.CurrentContext.Test.FullName} started");
        }

        protected abstract void Preconditions();
    }
}
