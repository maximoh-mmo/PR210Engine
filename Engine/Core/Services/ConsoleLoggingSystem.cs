
namespace Engine.Core.Services
{
    public class ConsoleLoggingSystem : ILoggingService
    {
        public void Log(string message, LogLevel level)
        {
            Console.ForegroundColor = GetConsoleColor(level);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}");
            Console.ResetColor();
        }

        public void Info(string message)
        {
            Log(message, LogLevel.Info);
        }

        public void Debug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public void Warning(string message)
        {
            Log(message, LogLevel.Warning);
        }

        public void Critical(string message)
        {
            Log(message, LogLevel.Critical);
        }

        private ConsoleColor GetConsoleColor(LogLevel level)
        {
            return level switch
            {
                LogLevel.Info => ConsoleColor.Cyan,
                LogLevel.Debug => ConsoleColor.Gray,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Critical => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
        }
    }
}
