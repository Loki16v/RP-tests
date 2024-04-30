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
    public class AdminSearchByQueryNunitTests : BaseNunitTest
    {
        protected override void Preconditions() { }

        private static readonly List<QueryAndResultNumberDataModel> SuperAdminQuery =
            TestDataUtility.GetListTestDataFromJson<QueryAndResultNumberDataModel>("SuperAdminQueryTestData.json");


        [Test, TestCaseSource(nameof(SuperAdminQuery))]
        public void Search_Admin_By_Query(QueryAndResultNumberDataModel data)
        {
            var userListResponse = Steps.AsAdminUser().SearchUsers<SearchUsersResponse>(data.Query).UserList;
            userListResponse.Should().Contain(x => x.Id.Equals(data.Number));
        }
    }
}