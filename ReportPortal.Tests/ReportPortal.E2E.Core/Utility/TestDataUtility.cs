using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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

        public static List<object[]> GetPrimitiveDataFromJson(string jsonFileName)
        {
            using var reader = new StreamReader($"{TestDataDefaultPath}/{jsonFileName}");
            var list = JsonConvert.DeserializeObject<List<object[]>>(reader.ReadToEnd());

            return list;
        }
    }
}
