using System.Collections.Generic;

namespace WebCLI.Core.Contracts
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        List<string> Messages { get; set; }
        object Response { get; set; }
        string ResponseType { get; set; }
    }
}