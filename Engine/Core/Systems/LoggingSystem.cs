using Engine.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Systems
{
    public class LoggingSystem
    {
        private readonly ILoggingService loggingService;

        public LoggingSystem(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public void Log(string message, LogLevel level)
        {
            loggingService.Log(message, level);
        }
    }
}
