using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace ReportPortal.E2E.Core.Logger
{
    public static class TestsLogger
    {
        public static ILoggerFactory LoggerFactory { get; }


        static TestsLogger()
        {
            LoggerFactory = TestsBootstrap.Instance.ServiceProvider.GetService<ILoggerFactory>();
        }

        public static ILogger Create<T>()
        {
            return Create($"{typeof(T).Name}");
        }

        public static ILogger Create(string name)
        {
            return LoggerFactory.CreateLogger($"{name} :");
        }
    }
}