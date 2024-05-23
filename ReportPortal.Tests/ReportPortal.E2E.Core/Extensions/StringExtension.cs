using System.Text.RegularExpressions;

namespace ReportPortal.E2E.Core.Extensions
{
    public static class StringExtension
    {
        public static string BindByPosition(this string url, params string[] parameters)
        {
            var regex = new Regex("{(.*?)}", RegexOptions.IgnoreCase);
            var match = regex.Match(url);
            var matchIndex = 0;

            while (match.Success)
            {
                url = url.Replace(match.Value, parameters[matchIndex++]);
                match = match.NextMatch();
            }

            return url;
        }
    }
}
