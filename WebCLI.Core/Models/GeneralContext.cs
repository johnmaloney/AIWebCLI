using WebCLI.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq; // Needed for ToArray()

namespace WebCLI.Core.Models
{
    public class GeneralContext : IContext, ICommandResult, IQueryResult
    {
        // Properties from IContext
        public string Identifier { get; private set; } // Assuming it's set internally or via constructor
        public List<string> Arguments { get; private set; } // Matches IContext
        public Dictionary<string, object> Options { get; private set; } // Matches IContext
        
        // Combined Response property for IContext and ICommandResult
        public object Response
        {
            get => CommandResult?.Response ?? QueryResult?.Data; // Use Data for QueryResult as per IQueryResult
            set // Change from private set to public set
            {
                if (CommandResult != null) CommandResult.Response = value;
                // For IQueryResult, 'Data' is the setter
            }
        }

        // Properties from ICommandResult and IQueryResult
        public bool Success { get => CommandResult?.Success ?? QueryResult?.Success ?? false; set { if (CommandResult != null) CommandResult.Success = value; if (QueryResult != null) QueryResult.Success = value; } }
        
        private List<string> _messages = new List<string>(); // Internal list for messages

        // Explicit implementation for IContext.Messages as string[]
        string[] IContext.Messages => _messages.ToArray();

        // Explicit implementation for ICommandResult.Messages as List<string>
        List<string> ICommandResult.Messages { get => _messages; set => _messages = value; }

        // Now IQueryResult also defines 'Messages', so we need to implement it explicitly or through a shared property.
        // We will make it explicit to match the List<string> type, similar to ICommandResult.
        List<string> IQueryResult.Messages { get => _messages; set => _messages = value; }

        public object Data // From IQueryResult
        {
            get => QueryResult?.Data;
            set { if (QueryResult != null) QueryResult.Data = value; } // IQueryResult.Data has a setter
        }

        public string ResponseType { get => CommandResult?.ResponseType ?? QueryResult?.ResponseType; set { if (CommandResult != null) CommandResult.ResponseType = value; if (QueryResult != null) QueryResult.ResponseType = value; } }

        // Original properties
        public ICommand Command { get; set; }
        public IQuery<object> Query { get; set; } // Fix CS0305: using object as placeholder
        public CommandResult CommandResult { get; set; }
        public IQueryResult QueryResult { get; set; }

        // Method from IContext
        public void AddMessage(params string[] messages)
        {
            if (messages != null) _messages.AddRange(messages);
            if (CommandResult != null && messages != null) CommandResult.Messages.AddRange(messages);
            if (QueryResult != null && messages != null) QueryResult.Messages.AddRange(messages); // Now IQueryResult has Messages
        }

        // Constructor to initialize Identifier, Arguments, Options
        public GeneralContext()
        {
            Identifier = Guid.NewGuid().ToString(); // Placeholder
            Arguments = new List<string>(); // Matches IContext
            Options = new Dictionary<string, object>(); // Matches IContext
        }
    }
}
