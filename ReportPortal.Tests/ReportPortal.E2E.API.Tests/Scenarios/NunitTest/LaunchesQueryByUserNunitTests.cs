using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.Scenarios.NunitTest.BaseTest;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Models.TestDataModel;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.API.Tests.Scenarios.NunitTest
{
    [Parallelizable(ParallelScope.All)]
    public class LaunchesQueryByUserNunitTests : BaseNunitTest
    {
        protected override void Preconditions() { }

        private static readonly List<string[]> LatestLaunchQueryTestData =
            TestDataUtility.GetTestDataFromJson<LatestLaunchQueryDataModel>("LatestLaunchQueryTestData.json").LatestLaunchQuery;
        private static readonly List<object[]> LaunchesQuery = TestDataUtility.GetPrimitiveDataFromJson("LaunchesQueryTestData.json");


        [Test, TestCaseSource(nameof(LaunchesQuery))]
        public void Filter_Demo_Launches_By_Query(string query, Int64 count)
        {
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter(ProjectName, query).GetAwaiter().GetResult()
                .GetResponse<GetLaunchesResponse>().Launches;
            launchesList.Should().HaveCount((int)count);
        }

        [Test, TestCaseSource(nameof(LatestLaunchQueryTestData))]
        public void Filter_Latest_Launch_By_Query(string query)
        {
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter(ProjectName, query).GetAwaiter().GetResult()
                .GetResponse<GetLaunchesResponse>().Launches.OrderBy(x => x.EndTime);
            var latestLaunchUuid = Steps.AsUser(NewUserCredentials).GetLatestLaunchByFilter(ProjectName, query).GetAwaiter()
                .GetResult().GetResponse<GetLaunchesResponse>().Launches.Single().Uuid;

            launchesList.Last().Uuid.Should().Be(latestLaunchUuid);
        }
    }
}