using Microsoft.Extensions.Logging;

using NLog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogSampleDI
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }

        public void DoAction(string name)
        {
            _logger.LogInformation(20, "Doing hard work! {Action}", name);
            _logger.LogTrace("trace");
            _logger.LogDebug("debug");
            _logger.LogError("error");
        }
    }
}
