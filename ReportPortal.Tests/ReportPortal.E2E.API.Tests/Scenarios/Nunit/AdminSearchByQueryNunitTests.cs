using FluentAssertions;
using NUnit.Framework;
using ReportPortal.E2E.API.Core.Business;
using ReportPortal.E2E.API.Core.Business.Models.Responses;
using ReportPortal.E2E.API.Tests.TestData;
using ReportPortal.E2E.Common.Extensions;

namespace ReportPortal.E2E.API.Tests.Scenarios.Nunit
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class AdminSearchByQueryNunitTests : BaseTest
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
