using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public class GeneralContext : APipeContext
    {
        public ICommand Command { get; set; }
        public WebCLI.Core.Models.IQuery<object> Query { get; set; } // Type changed to IQuery<object>
        public WebCLI.Core.Contracts.IQueryResult QueryResult { get; set; } // Added QueryResult
        public WebCLI.Core.Contracts.ICommandResult CommandResult { get; set; } // Added CommandResult
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
