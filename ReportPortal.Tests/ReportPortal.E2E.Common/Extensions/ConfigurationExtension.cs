using Microsoft.Extensions.Configuration;

namespace ReportPortal.E2E.Common.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetValueOrThrow(this IConfigurationSection section)
        {
            return section.Value ?? throw new Exception($"{section.Path} configuration section value is not set in appsettings.json");
        }
    }
}
