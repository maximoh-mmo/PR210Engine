using Engine.Core.Interfaces;
using Serilog;
using Serilog.Core;

namespace Engine.Core.Services
{
    public class SerilogLoggingService : ILoggingService
    {
        private ILogger _logger;

        public SerilogLoggingService()
        {
            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(Serilog.Events.LevelAlias.Minimum);
            _logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Console(outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public void LogTrace(string message, object[]? args = null)
        {
            _logger.Verbose(message, args);
        }

        public void LogDebug(string message, object[]? args = null)
        {
            _logger.Debug(message, args);
        }

        public void LogInfo(string message, object[]? args = null)
        {
            _logger.Information(message, args);
        }

        public void LogWarning(string message, object[]? args = null)
        {
            _logger.Warning(message, args);
        }

        public void LogError(string message, Exception? ex = null, object[]? args = null)
        {
            _logger.Error(message, args);
        }

        public void LogFatal(string message, Exception? ex = null, object[]? args = null)
        {
            _logger.Fatal(message, args);
        }

        public IDisposable BeginScope(string ctx)
        {
            return Serilog.Context.LogContext.PushProperty("Scope", ctx);
        }
    }
}