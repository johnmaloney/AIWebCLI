using System.Collections.Generic;

namespace WebCLI.Core.Models
{
    public class Query<TResult> : IQuery<TResult>
    {
        public string Name { get; }
        public Dictionary<string, string> Parameters { get; }

        public Query(string name, Dictionary<string, string> parameters = null)
        {
            Name = name;
            Parameters = parameters ?? new Dictionary<string, string>();
        }
    }
}
