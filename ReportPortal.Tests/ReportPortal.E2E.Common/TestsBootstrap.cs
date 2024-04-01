using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportPortal.E2E.Common.Extensions;
using ReportPortal.E2E.Common.HttpMessageHandlers;
using ReportPortal.E2E.Common.Logger;
using ReportPortal.E2E.Common.Models;
using Serilog;

namespace ReportPortal.E2E.Common
{
    public sealed class TestsBootstrap
    {
        private static readonly Lazy<TestsBootstrap> Inst = new(() => new TestsBootstrap(), true);

        public readonly IConfigurationRoot Configuration;

        private TestsBootstrap()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", true, false);

            Configuration = builder.Build();
            ServiceProvider = ConfigureServices();
        }

        public static TestsBootstrap Instance => Inst.Value;

        public IServiceProvider ServiceProvider { get; }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var defaultUser = new UserCredentials
            {
                UserName = Configuration.GetSection("DefaultUser:UserName").GetValueOrThrow(),
                Password = Configuration.GetSection("DefaultUser:DefaultPassword").GetValueOrThrow()
            };
            services.AddSingleton(defaultUser);

            var reportPortalConfig = new ReportPortalConfig
            {
                BaseUrl = Configuration.GetSection("ReportPortalConfig:BaseUrl").GetValueOrThrow(),
                ApiAuthUrl = Configuration.GetSection("ReportPortalConfig:ApiAuthUrl").GetValueOrThrow()
            };
            services.AddSingleton(reportPortalConfig);

            services.AddLogging(builder => builder.AddSerilog(SerilogFactory.CreateSerilogLogger(Configuration.GetSection("LogsPath").Value)));
            services.AddSingleton<ClientsHandler>();
            return services.BuildServiceProvider();
        }

    }
}