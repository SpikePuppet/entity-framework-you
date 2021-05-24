using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Atlas.Utilities
{
    public class ProcessProxy : IProcessProxy
    {
        internal const int DefaultProcessTimeoutMilliseconds = 10000;
        private readonly ILogger<ProcessProxy> _log;

        public ProcessProxy(ILogger<ProcessProxy> log)
        {
            _log = log;
        }

        public string Run(string path, List<string> arguments)
        {
            return Run(path, arguments, DefaultProcessTimeoutMilliseconds);
        }

        public string Run(string path, List<string> arguments, int timeout)
        {
            return Run(path, arguments, DefaultProcessTimeoutMilliseconds, ProcessProxyAutoWrapParameters.Triple);
        }

        public string Run(string path, List<string> arguments, int timeout, ProcessProxyAutoWrapParameters wrapParameters)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            if (arguments != null)
            {
                processStartInfo.Arguments = string.Join(" ", arguments.Select(x =>
                {
                    var parameter = x.Replace("\"", "\\\"")
                        .Replace("`", "\\`")
                        .Replace("!", "\"\"\"\"\"\"!\"\"\"\"\"\"");

                    switch (wrapParameters)
                    {
                        case ProcessProxyAutoWrapParameters.None:
                            break;
                        case ProcessProxyAutoWrapParameters.Single:
                            parameter = string.Format("\"{0}\"", parameter);
                            break;
                        case ProcessProxyAutoWrapParameters.Double:
                            parameter = string.Format("\"\"{0}\"\"", parameter);
                            break;
                        case ProcessProxyAutoWrapParameters.Triple:
                            parameter = string.Format("\"\"\"{0}\"\"\"", parameter);
                            break;
                    }

                    return parameter;
                }));
            }

            _log.LogDebug("Arguments for script: " + processStartInfo.Arguments);

            var process = Process.Start(processStartInfo);
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit(timeout);
            process.Close();

            _log.LogDebug("Output from script: " + output);

            if (!string.IsNullOrEmpty(error))
            {
                _log.LogError("Error from script: " + error);

                throw new ApplicationException("Script Error: " + error);
            }

            return output;
        }
    }
}
