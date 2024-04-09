using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;
using ReportPortal.E2E.Core.Helpers;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public Task<HttpResponseMessage> CreateUser(string userName, string password, string projectName, string projectRole = "MEMBER")
        {
            var requestBody = new
            {
                accountRole = "USER",
                defaultProject = projectName,
                email = $"{RandomValuesHelper.RandomString()}@mail.com",
                fullName = RandomValuesHelper.RandomString(),
                login = userName,
                password = password,
                projectRole = projectRole
            };
            var endpoint = Endpoints.Users;
            Log.LogInformation($"CreateUser '{userName}' as '{projectRole}' in '{projectName}'\n Method: Post\n Endpoint: {endpoint}");
            return _launchApiSteps.PostAsJsonAsync(endpoint, requestBody);
        }

        public Task<HttpResponseMessage> SearchUsers(string query)
        {
            var endpoint = Endpoints.SearchUsers + query;
            Log.LogInformation($"SearchUsers by query '{query}''\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.GetAsync(endpoint);
        }
    }
}