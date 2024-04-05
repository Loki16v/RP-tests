namespace ReportPortal.E2E.Core.Helpers
{
    public static class RandomValuesHelper
    {
        public static string RandomString(int length = 7)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
