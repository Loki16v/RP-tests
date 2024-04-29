using Newtonsoft.Json;

namespace ReportPortal.E2E.Core.Utility
{
    public static class TestDataUtility
    {
        private static readonly string TestDataDefaultPath = $"{AppContext.BaseDirectory}/TestData";

        public static List<T> GetListTestDataFromJson<T>(string jsonFileName)
        {
            using var reader = new StreamReader($"{TestDataDefaultPath}/{jsonFileName}");
            return JsonConvert.DeserializeObject<List<T>>(reader.ReadToEnd());
        }
    }
}
