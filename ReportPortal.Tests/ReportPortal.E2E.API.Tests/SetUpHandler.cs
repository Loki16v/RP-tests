using Microsoft.Extensions.Logging;
using ReportPortal.E2E.Core.Logger;

namespace ReportPortal.E2E.API.Tests
{
    public sealed class SetUpHandler
    {
        private readonly AutoResetEvent _waitHandler = new(true);
        private bool IsPreconditionDone { get; set; }
        private static readonly ILogger Log = TestsLogger.Create<SetUpHandler>();

        public void Do(IList<Action> functions)
        {
            _waitHandler.WaitOne();
            try
            {
                if (IsPreconditionDone)
                    return;
                Log.LogInformation("Start executing test preconditions.");
                functions.ToList().ForEach(func => func());
                IsPreconditionDone = true;
                Log.LogInformation("Finished executing test preconditions.");
            }
            catch (Exception e)
            {
                var message = GetErrorMsg(e);
                Log.LogInformation($"Thrown exception while preconditions were executing\n{message}");
                throw new Exception(message, e);
            }
            finally
            {
                _waitHandler.Set();
            }
        }

        private string GetErrorMsg(Exception exception)
        {
            return $"Marked Failed because of Setup Failed With:\n{exception.GetType().FullName}: {exception.Message}\n" +
                   $"StackTrace: {exception.StackTrace}";
        }
    }
}