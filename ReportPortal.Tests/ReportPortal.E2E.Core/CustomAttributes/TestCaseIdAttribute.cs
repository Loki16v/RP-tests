using NUnit.Framework;

namespace ReportPortal.E2E.Core.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestCaseIdAttribute : PropertyAttribute
    {
        public TestCaseIdAttribute(string id) : base("TestCaseId", id) { }
    }
}
