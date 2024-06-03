using ReportPortal.E2E.Core.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ReportPortal.E2E.Core.Extensions
{
    [Binding]
    public static class Transformations
    {
        [StepArgumentTransformation]
        public static List<LaunchModel> ParseTable(Table table)
        {
            return table.CreateSet<LaunchModel>().ToList();
        }

        [StepArgumentTransformation]
        public static bool IsEnabled(string state)
        {
            return state switch
            {
                "enabled" => true,
                "disabled" => false,
                _ => throw new NotSupportedException($"Expected 'enabled' or 'disabled' only, but was '{state}'")
            };
        }
    }
}
