using WebCLI.Core.Contracts;
using System.Collections.Generic;

namespace WebCLI.Core.Models
{
    public class GeneralContext : IContext, ICommandResult, IQueryResult
    {
        public ICommand Command { get; set; }
        public IQuery Query { get; set; }
        public CommandResult CommandResult { get; set; }
        public IQueryResult QueryResult { get; set; } // Will hold the generic QueryResult<TResult>
        public bool Success { get => CommandResult?.Success ?? QueryResult?.Success ?? false; set { if (CommandResult != null) CommandResult.Success = value; if (QueryResult != null) QueryResult.Success = value; } }
        public List<string> Messages { get => CommandResult?.Messages ?? QueryResult?.Messages ?? new List<string>(); set { if (CommandResult != null) CommandResult.Messages = value; if (QueryResult != null) QueryResult.Messages = value; } }
        public object Data { get => CommandResult?.Response ?? QueryResult?.Data; set { if (CommandResult != null) CommandResult.Response = value; if (QueryResult != null) QueryResult.Data = value; } }
        public string ResponseType { get => CommandResult?.ResponseType ?? QueryResult?.ResponseType; set { if (CommandResult != null) CommandResult.ResponseType = value; if (QueryResult != null) QueryResult.ResponseType = value; } }
    }
}
