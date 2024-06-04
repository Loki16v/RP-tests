using ReportPortal.Serilog;
using Serilog;

namespace ReportPortal.E2E.Core.Logger
{
    internal static class SerilogFactory
    {
        private const string OutputTemplate =
            "{Timestamp:HH:mm:ss.fff} {Level:u3} {SourceContext} {Message} {Scope}{NewLine}{Exception}{NewLine}";

        public static ILogger CreateSerilogLogger(string path, string fileName = "TestLogs.txt")
        {
            try
            {
                var filePath = Path.Combine(Path.GetFullPath(path), fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                return new LoggerConfiguration().MinimumLevel.Error()
                    .WriteTo.Logger(l => l.WriteTo.File(
                        filePath, outputTemplate: OutputTemplate))
                    .WriteTo.ReportPortal()
                    .CreateLogger();
            }
            catch (Exception)
            {
                return Log.Logger;
            }
        }
    }
}
