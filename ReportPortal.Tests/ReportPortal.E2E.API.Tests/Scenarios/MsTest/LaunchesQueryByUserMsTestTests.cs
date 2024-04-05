using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.BaseTest;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.Core.Extensions;

namespace ReportPortal.E2E.API.Tests.Scenarios.MsTest
{
    [TestClass]
    public class LaunchesQueryByUserMsTestTests : MsTestBaseTest
    {
        [TestMethod]
        [DataTestMethod]
        [DynamicData(nameof(GetLaunchesQuery), DynamicDataSourceType.Method)]
        public void Filter_Demo_Launches_By_Query(string query, int count)
        {
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter(ProjectName, query).GetAwaiter().GetResult()
                .GetResponse<GetLaunchesResponse>().Launches;
            launchesList.Should().HaveCount(count);
        }

        [TestMethod]
        [DataTestMethod]
        [DynamicData(nameof(GetLatestLaunchQuery), DynamicDataSourceType.Method)]
        public void Filter_Latest_Launch_By_Query(string query)
        {
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter(ProjectName, query).GetAwaiter().GetResult()
                .GetResponse<GetLaunchesResponse>().Launches.OrderBy(x => x.EndTime);
            var latestLaunchUuid = Steps.AsUser(NewUserCredentials).GetLatestLaunchByFilter(ProjectName, query).GetAwaiter()
                .GetResult().GetResponse<GetLaunchesResponse>().Launches.Single().Uuid;

            launchesList.Last().Uuid.Should().Be(latestLaunchUuid);
        }


        private static List<string[]> GetLatestLaunchQuery()
        {
            return TestData.QueryTestData.LatestLaunchQuery;
        }
        private static List<object[]> GetLaunchesQuery()
        {
            return TestData.QueryTestData.LaunchesQuery;
        }
    }
}
