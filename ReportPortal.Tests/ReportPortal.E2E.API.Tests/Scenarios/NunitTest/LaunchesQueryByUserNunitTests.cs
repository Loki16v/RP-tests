using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.Scenarios.NunitTest.BaseTest;
using ReportPortal.E2E.Core.Models.TestDataModel;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.API.Tests.Scenarios.NunitTest
{
    [Parallelizable(ParallelScope.All)]
    public class LaunchesQueryByUserNunitTests : BaseNunitTest
    {
        protected override void Preconditions() { }

        private static readonly List<string[]> LatestLaunchQueryTestData =
            TestDataUtility.GetListTestDataFromJson<string[]>("LatestLaunchQueryTestData.json");
        private static readonly List<QueryAndResultNumberDataModel> LaunchesQuery =
            TestDataUtility.GetListTestDataFromJson<QueryAndResultNumberDataModel>("LaunchesQueryTestData.json");


        [Test, TestCaseSource(nameof(LaunchesQuery))]
        public void Filter_Demo_Launches_By_Query(QueryAndResultNumberDataModel data)
        {
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter<GetLaunchesResponse>(ProjectName, data.Query).Launches;
            launchesList.Should().HaveCount(data.Number);
        }

        [Test, TestCaseSource(nameof(LatestLaunchQueryTestData))]
        public void Filter_Latest_Launch_By_Query(string query)
        {
            var launchesList = Steps.AsUser(NewUserCredentials).GetLaunchesByFilter<GetLaunchesResponse>(ProjectName, query).Launches.OrderBy(x => x.EndTime);
            var latestLaunchUuid = Steps.AsUser(NewUserCredentials).GetLatestLaunchByFilter<GetLaunchesResponse>(ProjectName, query).Launches.Single().Uuid;

            launchesList.Last().Uuid.Should().Be(latestLaunchUuid);
        }
    }
}