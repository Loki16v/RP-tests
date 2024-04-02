namespace ReportPortal.E2E.API.Tests.TestData
{
    public static class QueryTestData
    {
        internal static List<string[]> LatestLaunchQuery { get; } = new()
        {
            new[] { "?filter.eq.status=passed" },
            new[] { "?filter.eq.status=failed" },
            new[] { "?filter.eq.hasRetries=true" },
            new[] { "?filter.eq.hasRetries=false" },
            new[] { "?filter.eq.name=Demo Api Tests" }
        };

        internal static List<object[]> LaunchesQuery { get; } = new()
        {
            new object[] { "?filter.eq.status=passed", 1 },
            new object[] { "?filter.eq.status=failed", 4 },
            new object[] { "?filter.eq.hasRetries=true", 4 },
            new object[] { "?filter.eq.hasRetries=false", 1 },
            new object[] { "?filter.eq.name=Demo Api Tests", 5 }
        };

        internal static List<object[]> SuperAdminQuery { get; } = new()
        {
            new object[] { "?filter.eq.email=superadminemail@domain.com", 1 },
            new object[] { "?filter.eq.fullName=tester", 1 },
            new object[] { "?filter.eq.role=ADMINISTRATOR", 1 },
            new object[] { "?filter.eq.type=INTERNAL", 1 },
            new object[] { "?filter.eq.user=superadmin", 1 }
        };
    }
}