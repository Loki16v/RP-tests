using ReportPortal.E2E.API.Business.Models;

namespace ReportPortal.E2E.Core.Extensions
{
    public static class ResponseStatusExtension
    {
        public static int GetCode(this ResponseStatus status)
        {
            return (int)status;
        }
    }
}
