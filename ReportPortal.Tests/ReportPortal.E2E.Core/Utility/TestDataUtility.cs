using Microsoft.Extensions.Configuration;

namespace ReportPortal.E2E.Core.Utility
{
    public static class TestDataUtility
    {
        private static readonly string TestDataDefaultPath = $"{AppContext.BaseDirectory}/TestData";

        public static T GetTestDataFromJson<T>(string jsonFileName)
        {
            return new ConfigurationBuilder()
                .SetBasePath(TestDataDefaultPath)
                .AddJsonFile(jsonFileName)
                .Build().Get<T>();
        }
    }
}
