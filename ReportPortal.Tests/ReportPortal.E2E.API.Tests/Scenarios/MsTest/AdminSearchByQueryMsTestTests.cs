using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.Scenarios.MsTest.BaseTest;
using ReportPortal.E2E.Core.Extensions;


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
            var userListResponse = Steps.AsAdminUser().SearchUsers(query).GetAwaiter().GetResult().GetResponse<SearchUsersResponse>().UserList;
            userListResponse.Should().Contain(x => x.Id.Equals(id));
        }

        private static List<object[]> GetAdminSearchQuery()
        {
            return TestData.QueryTestData.SuperAdminQuery;
        }
    }
}