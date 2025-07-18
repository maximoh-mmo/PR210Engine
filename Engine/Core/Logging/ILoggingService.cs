namespace Engine.Core.Logging
{
    public interface ILoggingService
    {
        public void LogTrace(string message, object[]? args = null);
        public void LogDebug(string message, object[]? args = null);
        public void LogInfo(string message, object[]? args = null);
        public void LogWarning(string message, object[]? args = null);
        public void LogError(string message, Exception? ex = null, object[]? args = null);
        public void LogFatal(string message, Exception? ex = null, object[]? args = null);

        public IDisposable BeginScope(string ctx);
    }

}
