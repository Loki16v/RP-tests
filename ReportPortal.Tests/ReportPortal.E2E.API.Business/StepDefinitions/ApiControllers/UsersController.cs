using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;
using ReportPortal.E2E.Core.Helpers;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public Task<HttpResponseMessage> CreateUser(string userName, string password, string projectName,
            string email, string fullName, string projectRole, string accountRole)
        {
            var requestBody = new
            {
                accountRole = accountRole ?? "USER",
                defaultProject = projectName,
                email = email ?? $"{RandomValuesHelper.RandomString()}@mail.com",
                fullName = fullName ?? RandomValuesHelper.RandomString(),
                login = userName,
                password = password,
                projectRole = projectRole
            };
            var endpoint = Endpoints.Users;
            Log.LogInformation($"CreateUser '{userName}' as '{projectRole}' in '{projectName}'\n Method: Post\n Endpoint: {endpoint}");
            return _launchApiSteps.PostAsJsonAsync(endpoint, requestBody);
        }

        public Task<HttpResponseMessage> SearchUsers(string query = null)
        {
            var endpoint = query is null ? Endpoints.SearchUsers : Endpoints.SearchUsers + query;
            Log.LogInformation($"SearchUsers by query\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.GetAsync(endpoint);
        }
    }
}