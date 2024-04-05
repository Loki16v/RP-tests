using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.BaseTest;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.TestData;
using ReportPortal.E2E.Core.Extensions;

namespace ReportPortal.E2E.API.Tests.Scenarios.Nunit
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class LaunchesQueryByAdminNunitTests : BaseNunitTest
    {
        protected override void Preconditions() { }

        [Test, TestCaseSource(typeof(QueryTestData), nameof(QueryTestData.LaunchesQuery))]
        public void Filter_Demo_Launches_By_Query(string query, int count)
        {
            var launchesList = Steps.AsAdminUser().GetLaunchesByFilter(ProjectName, query).GetAwaiter().GetResult()
                .GetResponse<GetLaunchesResponse>().Launches;
            launchesList.Should().HaveCount(count);
        }

        [Test, TestCaseSource(typeof(QueryTestData), nameof(QueryTestData.LatestLaunchQuery))]
        public void Filter_Latest_Launch_By_Query(string query)
        {
            var launchesList = Steps.AsAdminUser().GetLaunchesByFilter(ProjectName, query).GetAwaiter().GetResult()
                .GetResponse<GetLaunchesResponse>().Launches.OrderBy(x => x.EndTime);
            var latestLaunchUuid = Steps.AsAdminUser().GetLatestLaunchByFilter(ProjectName, query).GetAwaiter()
                .GetResult().GetResponse<GetLaunchesResponse>().Launches.Single().Uuid;

            launchesList.Last().Uuid.Should().Be(latestLaunchUuid);
        }
    }
}
