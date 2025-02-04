namespace Engine.Core.Services
{
    public enum LogLevel
    {
        Info,
        Debug,
        Warning,
        Critical
    }
    public interface ILoggingService
    {
        public void Log(string message, LogLevel level);
    }
}
