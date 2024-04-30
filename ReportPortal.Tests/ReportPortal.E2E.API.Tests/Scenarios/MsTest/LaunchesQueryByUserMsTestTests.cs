using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.Scenarios.MsTest.BaseTest;
using ReportPortal.E2E.Core.Models.TestDataModel;
using ReportPortal.E2E.Core.Utility;

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
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter<GetLaunchesResponse>(ProjectName, query).Launches;
            launchesList.Should().HaveCount(count);
        }

        [TestMethod]
        [DataTestMethod]
        [DynamicData(nameof(GetLatestLaunchQuery), DynamicDataSourceType.Method)]
        public void Filter_Latest_Launch_By_Query(string query)
        {
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter<GetLaunchesResponse>(ProjectName, query)
                .Launches.OrderBy(x => x.EndTime);
            var latestLaunchUuid = Steps.AsUser(NewUserCredentials).GetLatestLaunchByFilter<GetLaunchesResponse>(ProjectName, query).Launches.Single().Uuid;

            launchesList.Last().Uuid.Should().Be(latestLaunchUuid);
        }


        private static IEnumerable<string[]> GetLatestLaunchQuery()
        {
            return TestDataUtility.GetListTestDataFromJson<string[]>("LatestLaunchQueryTestData.json");
        }
        private static IEnumerable<object[]> GetLaunchesQuery()
        {
            return TestDataUtility.GetListTestDataFromJson<QueryAndResultNumberDataModel>("LaunchesQueryTestData.json")
                .Select(item => new object[]{item.Query, item.Number}).ToList();
        }
    }
}