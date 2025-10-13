using Serilog;
using System.Diagnostics;

namespace DocManagementSystem.Common.Helpers
{
    public class FunctionTracer : IDisposable
    {
        private readonly Stopwatch? _stopwatch;
        private readonly string _label;
        private readonly bool _loginfile;
        private readonly ILogger _logger;

        public FunctionTracer(bool loginfile = false, string alias = "")
        {
            _logger = Log.Logger;
            _loginfile = loginfile;

            string className = "UnknownClass";
            string methodName = "UnknownMethod";

            try
            {
                var stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(1);
                var method = frame?.GetMethod();

                if (method != null)
                {
                    className = method.DeclaringType?.Name ?? className;
                    methodName = method.Name ?? methodName;

                    if (method.DeclaringType?.FullName?.ToLower().Contains("aspnetcore") == true)
                    {
                        className = method.DeclaringType.DeclaringType?.Name ?? className;
                        methodName = "ScreenBinding";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "FunctionTracer failed to resolve stack trace.");
            }

            alias = !string.IsNullOrEmpty(alias) ? alias + " -> " : string.Empty;
            _label = $"{alias}{className} -> {methodName}";

            if (_loginfile)
            {
                _stopwatch = Stopwatch.StartNew();
                _logger.Information($"{_label} - Execution Start");
            }
        }

        public void Dispose()
        {
            if (_loginfile && _stopwatch != null)
            {
                _stopwatch.Stop();
                _logger.Information($"{_label} - Total Execution Time: {_stopwatch.Elapsed.TotalSeconds} sec(s)");
            }
        }
    }
}
