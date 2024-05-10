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
    public class AdminSearchByQueryMsTestTests : MsTestBaseTest
    {
        [TestMethod]
        [DataTestMethod]
        [DynamicData(nameof(GetAdminSearchQuery), DynamicDataSourceType.Method)]
        public void Search_Admin_By_Query(string query, int id)
        {
            var userListResponse = Steps.AsAdminUser().SearchUsers<SearchUsersResponse>(query).UserList;
            userListResponse.Should().Contain(x => x.Id.Equals(id));
        }

        private static IEnumerable<object[]> GetAdminSearchQuery()
        {
            return TestDataUtility.GetListTestDataFromJson<QueryAndResultNumberDataModel>("SuperAdminQueryTestData.json")
                .Select(item => new object[]{item.Query, item.Number});
        }
    }
}