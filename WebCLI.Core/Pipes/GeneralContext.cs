using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public class GeneralContext : APipeContext
    {
        public Command Command { get; set; }
        public Query Query { get; set; }
        public ILogger Logger { get; set; } // Example for logging within pipes
        // Add any other general-purpose properties that pipes might need

        public GeneralContext()
        {
            // Initialize with a default logger or inject one
            Logger = new ConsoleLogger(); 
        }

        public override void AddMessage(params string[] messages)
        {
            throw new System.NotImplementedException();
        }
    }

    // Simple console logger for demonstration
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            System.Console.WriteLine($"[LOG] {message}");
        }
    }

    public interface ILogger
    {
        void Log(string message);
    }
}
