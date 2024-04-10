using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.Scenarios.NunitTest.BaseTest;
using ReportPortal.E2E.API.Tests.TestData;
using ReportPortal.E2E.Core.Extensions;

namespace ReportPortal.E2E.API.Tests.Scenarios.NunitTest
{
    [Parallelizable(ParallelScope.All)]
    public class AdminSearchByQueryNunitTests : BaseNunitTest
    {
        protected override void Preconditions() { }


        [Test, TestCaseSource(typeof(QueryTestData), nameof(QueryTestData.SuperAdminQuery))]
        public void Search_Admin_By_Query(string query, int id)
        {
            var userListResponse = Steps.AsAdminUser().SearchUsers(query).GetAwaiter().GetResult().GetResponse<SearchUsersResponse>().UserList;
            userListResponse.Should().Contain(x => x.Id.Equals(id));
        }
    }
}