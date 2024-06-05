using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;
using ReportPortal.E2E.Core.Models;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.Core.Helpers
{
    public static class JiraHelper
    {
        private static readonly JiraConfig JiraConfig = TestsBootstrap.Instance.ServiceProvider.GetRequiredService<JiraConfig>();

        public static void UpdateTestCaseStatus(string id, TestStatus status)
        {
            var body = new
            {
                fields = new Dictionary<string, Dictionary<string, string>>
                {
                    { JiraConfig.IsAutomatedField, new Dictionary<string, string>
                        {
                            {"value", Convert.ToString(status == TestStatus.Passed? status : "Failed")}
                        }
                    }
                }
            };
            ApiClient.Get(JiraConfig.BaseUrl).Put(string.Format(JiraConfig.IssueEndpoint, id), body, new List<KeyValuePair<string, string>>
            {
                new("Authorization", "Basic " + Convert
                    .ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{JiraConfig.Login}:{JiraConfig.Password}")))
            });
        }
    }
}
