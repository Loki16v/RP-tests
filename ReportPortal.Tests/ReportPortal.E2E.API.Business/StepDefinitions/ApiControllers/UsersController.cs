using Microsoft.Extensions.Logging;
using ReportPortal.E2E.API.Business.Models;
using ReportPortal.E2E.Core.Enums;
using ReportPortal.E2E.Core.Extensions;
using ReportPortal.E2E.Core.Helpers;

namespace ReportPortal.E2E.API.Business.StepDefinitions.ApiControllers
{
    public partial class ReportPortalApiSteps
    {
        public void CreateUser(string userName, string password, string projectName,
            string email, string fullName, ProjectRole projectRole, AccountRole accountRole)
        {
            var requestBody = new
            {
                accountRole = accountRole.GetName(),
                defaultProject = projectName,
                email = email ?? $"{RandomValuesHelper.RandomString()}@mail.com",
                fullName = fullName ?? RandomValuesHelper.RandomString(),
                login = userName,
                password = password,
                projectRole = projectRole.GetName()
            };
            var endpoint = Endpoints.Users;
            Log.LogInformation($"CreateUser '{userName}' as '{projectRole}' in '{projectName}'\n Method: Post\n Endpoint: {endpoint}");
            _launchApiSteps.Post(endpoint, requestBody);
        }

        public T SearchUsers<T>(string query = null)
        {
            var endpoint = query is null ? Endpoints.SearchUsers : Endpoints.SearchUsers + query;
            Log.LogInformation($"SearchUsers by query\n Method: Get\n Endpoint: {endpoint}");
            return _launchApiSteps.Get<T>(endpoint);
        }
    }
}