using ReportPortal.E2E.Core.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ReportPortal.E2E.Core.Extensions
{
    [Binding]
    public class Transformations
    {
        [StepArgumentTransformation]
        public List<LaunchModel> ParseTable(Table table)
        {
            return table.CreateSet<LaunchModel>().ToList();
        }

        [StepArgumentTransformation]
        public bool IsEnabled(string state)
        {
            return state switch
            {
                "enabled" => true,
                "disabled" => false,
                _ => throw new Exception($"Expected 'enabled' or 'disabled' only, but was '{state}'")
            };
        }
    }
}
