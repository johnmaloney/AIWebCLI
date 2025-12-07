using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    public class QueryResult<T> : IQueryResult
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public T Data { get; set; }
        public string ResponseType { get; set; }

        public QueryResult(bool success, string message, T data, string responseType = "json")
        {
            Success = success;
            Messages.Add(message);
            Data = data;
            ResponseType = responseType;
        }

        public QueryResult()
        {
            // Default constructor for deserialization or easy initialization
        }

        // Explicit interface implementation for non-generic IQueryResult
        object IQueryResult.Data => Data;
    }
}
