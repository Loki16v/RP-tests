using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.Scenarios.NunitTest.BaseTest;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Utility;

namespace ReportPortal.E2E.API.Tests.Scenarios.NunitTest
{
    [Parallelizable(ParallelScope.All)]
    public class AdminSearchByQueryNunitTests : BaseNunitTest
    {
        protected override void Preconditions() { }

        private static readonly List<object[]> SuperAdminQuery = TestDataUtility.GetPrimitiveDataFromJson("SuperAdminQueryTestData.json");


        [Test, TestCaseSource(nameof(SuperAdminQuery))]
        public void Search_Admin_By_Query(string query, Int64 id)
        {
            var userListResponse = Steps.AsAdminUser().SearchUsers(query).GetAwaiter().GetResult().GetResponse<SearchUsersResponse>().UserList;
            userListResponse.Should().Contain(x => x.Id.Equals((int)id));
        }
    }
}