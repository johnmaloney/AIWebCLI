using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    public class QueryResult : IQueryResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public QueryResult(bool isSuccessful, string message, object data = null)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data;
        }
    }
}
