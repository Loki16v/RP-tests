using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.Scenarios.MsTest.BaseTest;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Utility;


namespace ReportPortal.E2E.API.Tests.Scenarios.MsTest
{
    [TestClass]
    public class AdminSearchByQueryMsTestTests : MsTestBaseTest
    {
        [TestMethod]
        [DataTestMethod]
        [DynamicData(nameof(GetAdminSearchQuery), DynamicDataSourceType.Method)]
        public void Search_Admin_By_Query(string query, Int64 id)
        {
            var userListResponse = Steps.AsAdminUser().SearchUsers(query).GetAwaiter().GetResult().GetResponse<SearchUsersResponse>().UserList;
            userListResponse.Should().Contain(x => x.Id.Equals((int)id));
        }

        private static List<object[]> GetAdminSearchQuery()
        {
            return TestDataUtility.GetPrimitiveDataFromJson("SuperAdminQueryTestData.json");
        }
    }
}