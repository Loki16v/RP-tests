using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ReportPortal.E2E.Common.Logger;

namespace ReportPortal.E2E.API.Core.Business
{
    public abstract class BaseTest
    {
        protected ILogger Log { get; }

        private readonly SetUpHandler _setUpHandler = new();

        protected BaseTest()
        {
            Log = TestsLogger.Create<BaseTest>();
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
