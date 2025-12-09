using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public class GeneralContext : APipeContext
    {
        public ICommand Command { get; set; } // Changed from 'Command' to 'ICommand'
        public Query<object> Query { get; set; }
        public ILogger Logger { get; set; }

        public GeneralContext()
        {
            Logger = new ConsoleLogger();
        }

        public override void AddMessage(params string[] messages)
        {
            throw new System.NotImplementedException();
        }
    }

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
