using System.Reflection;
using System.Text.Json.Serialization;

namespace ReportPortal.E2E.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetName(this Enum value)
        {
            var jsonAttribute = value.GetType().GetMember(value.ToString()).Single()
                .GetCustomAttributes<JsonPropertyNameAttribute>().FirstOrDefault();
            return jsonAttribute != null ? jsonAttribute.Name : value.ToString();
        }
    }
}
