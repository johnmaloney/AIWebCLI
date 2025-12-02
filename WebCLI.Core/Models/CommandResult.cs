using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    public class CommandResult : ICommandResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public CommandResult(bool isSuccessful, string message, Dictionary<string, object> data = null)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data ?? new Dictionary<string, object>();
        }
    }
}
