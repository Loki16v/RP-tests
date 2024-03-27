using System.Text.RegularExpressions;

namespace ReportPortal.E2E.UI.Framework.Extensions
{
    public static class StringExtension
    {
        public static string BindByPosition(this string url, object[] parameters)
        {
            var regex = new Regex("{(.*?)}", RegexOptions.IgnoreCase);
            var match = regex.Match(url);
            var matchIndex = 0;

            while (match.Success)
            {
                url = url.Replace(match.Value, parameters[matchIndex++].ToString());
                match = match.NextMatch();
            }

            return url;
        }
    }
}
