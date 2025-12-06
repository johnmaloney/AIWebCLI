using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public object Response { get; set; }
        public string ResponseType { get; set; }

        public CommandResult(bool success, string message, object response = null, string responseType = "text")
        {
            Success = success;
            Messages = new List<string> { message };
            Response = response;
            ResponseType = responseType;
        }
    }
}
