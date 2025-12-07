using System.Collections.Generic;

namespace WebCLI.Core.Contracts
{
    public interface IQueryResult
    {
        bool Success { get; set; }
        List<string> Messages { get; set; }
        object Data { get; set; }
        string ResponseType { get; set; }
    }
}
